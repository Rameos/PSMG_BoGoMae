using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

        private float gameTime = 500f;
		private RefugeeMovement refugeeMovement;
		private int transmitterCounter;
		private string deadPlayer = null;
		private string winner = null;
        private Rect gameTimeGUIPosition = new Rect(Screen.width-200f, 50f, 150f, 100f);

		// Use this for initialization
		void Start ()
		{
				GameeventManager.transmitterIsDestroydHandler += reactOnTransmitterDestroyd;
				GameeventManager.onGoalReachedHandler += reactOnGoalReached;
                GameeventManager.onDroneSetSlowTrapHandler += reactOnSetSlowTrap;
                GameeventManager.onShowEnemyHandler += reactOnShowEnemy;
				GameeventManager.onPlayerDiedHandler += reactOnPlayerDied;
				transmitterCounter = GameObject.FindGameObjectsWithTag ("Transmitter").Length;
				Debug.Log (transmitterCounter);
		}

		// Update is called once per frame
		void Update ()
		{
            Countdown();

		}



        void OnGui()
        {
            GUI.Box(gameTimeGUIPosition, "Speed-Boost Zeit: " + gameTime.ToString("0"));
        }

        private void Countdown()
        {
            gameTime -= Time.deltaTime;
        }

		private void reactOnGoalReached ()
		{
				Debug.Log ("Flüchtling erreicht Ziel, Sieg, hurra!");
		}

		private void reactOnTransmitterDestroyd ()
		{
				Debug.Log ("in react on transmitter destroyd");
				transmitterCounter--;
				if (transmitterCounter == 0) {
						Debug.Log ("Refugee hat gewonnen | alle transmitter kaputt");
				}
		}

		private void reactOnPlayerDied (GameObject gameObject)
		{
				Debug.Log ("GameLogic.reactOnPlayerDied: " + gameObject.name);
				networkView.RPC ("PlayerDied", RPCMode.All, gameObject.tag);
		}


        private void reactOnSetSlowTrap()
        {
				networkView.RPC ("SlowDownRefugee", RPCMode.All, null);
        }


        private void reactOnShowEnemy()
        {
            //leuchtkegel in refugee script anzeigen
        }

		[RPC]
		public void PlayerDied (string gameOjectTag)
		{
				Debug.Log ("RPC PlayerDied");
				if (gameOjectTag == Config.DRONE_TAG) {
						Debug.Log ("1");
						deadPlayer = gameOjectTag;
						winner = "Refugee";
				} else if (gameOjectTag == Config.REFUGEE_TAG) {
						Debug.Log ("2");
						deadPlayer = gameOjectTag;
						winner = "Drone";
				}
		}

		[RPC]
		public void SlowDownRefugee ()
		{
			Debug.Log ("RPC SlowDownRefugee");

			float speed = 1f;
			GameObject refugee = GameObject.FindGameObjectWithTag(Config.REFUGEE_TAG);
			refugee.GetComponent<RefugeeMovement>().setMovementSpeedTo(speed);

		}

		void OnGUI ()
		{
				if (deadPlayer != null) {
					GUI.Label (new Rect (10, 10, 300, 20), winner + " won the game, " + deadPlayer + " lost!");
				}
		}
}
