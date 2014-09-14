using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

		private float gameTime = 240f;
		private string gameTimeString;
		private float minutes;
		private float seconds;
		private RefugeeMovement refugeeMovement;
		private int transmitterCounter;
		private string deadPlayer = null;
		private string winner = null;
		private Rect gameTimeGUIPosition = new Rect (Screen.width - 200f, 50f, 150f, 25f);
		private bool showRefugeeTrace = false;

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
				if (Network.connections.Length == 1) {
						Countdown ();
				}
                if (showRefugeeTrace)
                {
                    showRefugeeTraceWhileLeftEyeClosed();
                }
		}

		private void Countdown ()
		{
				gameTime -= Time.deltaTime;
				if (gameTime >= 60f) {
						minutes = gameTime / 60;
				} else {

						minutes = 0;
				}
				if (seconds <= 9) {
						seconds = gameTime % 60;
						gameTimeString = minutes.ToString ("0") + ":0" + seconds.ToString ("0");
				} else {

						seconds = gameTime % 60;
						gameTimeString = minutes.ToString ("0") + ":" + seconds.ToString ("0");
				}

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
						networkView.RPC ("CrashDrone", RPCMode.All, null);
				}
		}

		private void reactOnPlayerDied (GameObject gameObject)
		{
				Debug.Log ("GameLogic.reactOnPlayerDied: " + gameObject.name);
				networkView.RPC ("PlayerDied", RPCMode.All, gameObject.tag);
		}

		private void reactOnSetSlowTrap ()
		{
				networkView.RPC ("SlowDownRefugee", RPCMode.All, null);
		}

		private void reactOnShowEnemy ()
		{
				//leuchtkegel in refugee script anzeigen
				networkView.RPC ("ShowRefugeeTrace", RPCMode.All, null);
		}

		[RPC]
		public void CrashDrone ()
		{
				GameObject drone = GameObject.FindGameObjectWithTag (Config.DRONE_TAG);
				drone.rigidbody.useGravity = true;
				drone.rigidbody.freezeRotation = false;
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
		public void ShowRefugeeTrace ()
		{
            
				showRefugeeTrace = !showRefugeeTrace;  
				GameObject refugee = GameObject.FindGameObjectWithTag (Config.REFUGEE_TAG);

 /*
				if (showRefugeeTrace) {
						// evtl nach kurzer zeit wieder deaktivieren
						refugee.transform.FindChild ("TraceLight").gameObject.SetActive (true);
                        Debug.Log("show == true");
				} else {
						refugee.transform.FindChild ("TraceLight").gameObject.SetActive (false);
				}
              */
		}

        private void showRefugeeTraceWhileLeftEyeClosed()
        {

            GameObject refugee = GameObject.FindGameObjectWithTag(Config.REFUGEE_TAG);
            if (gazeModel.diamLeftEye == 0)
            {
                refugee.transform.FindChild("TraceLight").gameObject.SetActive(true);
                Debug.Log("show == true");

            }
            else
            {
                refugee.transform.FindChild("TraceLight").gameObject.SetActive(false);

            }

            
        }

		[RPC]
		public void SlowDownRefugee ()
		{
				Debug.Log ("RPC SlowDownRefugee");

				float speed = 1f;
				GameObject refugee = GameObject.FindGameObjectWithTag (Config.REFUGEE_TAG);
				refugee.GetComponent<RefugeeMovement> ().setMovementSpeedTo (speed);

		}

		void OnGUI ()
		{
				if (gameTime > 0) {
						GUI.Box (gameTimeGUIPosition, "Zeit: " + gameTimeString);

				} else {
						gameTimeString = "0:00";
						GUI.Box (gameTimeGUIPosition, "Zeit: " + gameTimeString);
						GUI.Label (new Rect (10, 10, 300, 20), "Time is over! Drone won the game, Refugee lost!");
						Application.LoadLevel ("DroneWon");
				}

				if (deadPlayer != null) {
						//GUI.Label (new Rect (10, 10, 300, 20), winner + " won the game, " + deadPlayer + " lost!");
						if (deadPlayer == Config.REFUGEE_TAG) {
								Application.LoadLevel ("DroneWon");
						} else if (deadPlayer == Config.DRONE_TAG) {
								Application.LoadLevel ("RefugeeWon");
						}
				}
		}
}
