using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{


		private RefugeeMovement refugeeMovement;
		private int transmitterCounter;
		private string deadPlayer = "";
		private string winner = "";

		// Use this for initialization
		void Start ()
		{
				GameeventManager.transmitterIsDestroydHandler += reactOnTransmitterDestroyd;
				GameeventManager.onGoalReachedHandler += reactOnGoalReached;
				GameeventManager.onPlayerDiedHandler += reactOnPlayerDied;
				transmitterCounter = GameObject.FindGameObjectsWithTag ("Transmitter").Length;
				Debug.Log (transmitterCounter);
		}



		// Update is called once per frame
		void Update ()
		{


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

		void OnGUI ()
		{
				if (deadPlayer != "") {
					GUI.Label (new Rect (10, 10, 300, 20), winner + " won the game, " + deadPlayer + " lost!");
				}
		}
}
