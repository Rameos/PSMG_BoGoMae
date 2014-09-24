using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

		private float gameTime = 540f;
		private string gameTimeString;
		private float minutes;
		private float seconds;
		private RefugeeMovement refugeeMovement;
		private int transmitterCounter;
		private string deadPlayer = null;
		private string winner = null;
		private Rect gameTimeGUIPosition = new Rect (Screen.width - 200f, 50f, 150f, 25f);
		private bool showMenu = false;
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
		}

		// Update is called once per frame
		void Update ()
		{
				if (Network.connections.Length == 1) {
						if (Network.isServer) {
								GameObject refugee = GameObject.FindGameObjectWithTag (Config.REFUGEE_TAG);
								refugee.transform.FindChild ("Main Camera").gameObject.SetActive (false);
						}
						Countdown ();
				}
				if (showRefugeeTrace) {
						showRefugeeTraceWhileLeftEyeClosed ();
				}
				ShowMenu ();
				CheckPlayersAlive ();
		}

		void CheckPlayersAlive ()
		{
				if (deadPlayer != null) {
						if (deadPlayer == Config.REFUGEE_TAG) {
								networkView.RPC ("QuitGame", RPCMode.All, "DroneWon");
						} else if (deadPlayer == Config.DRONE_TAG) {
								networkView.RPC ("QuitGame", RPCMode.All, "RefugeeWon");
						}
				}
		}
    
		private void ShowMenu ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						showMenu = !showMenu;
				}

				if (Input.GetKeyDown (KeyCode.B)) {
						if (Network.isClient)
								Application.LoadLevel ("DroneWon");
						if (Network.isServer)
								Application.LoadLevel ("RefugeeWon");
				} else if (Input.GetKeyDown (KeyCode.F)) {
						showMenu = !showMenu;
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
				deadPlayer = "Drone";
		}

		private void reactOnTransmitterDestroyd ()
		{
				transmitterCounter--;
				if (transmitterCounter == 0) {
						networkView.RPC ("CrashDrone", RPCMode.All, null);
				}
		}

		private void reactOnPlayerDied (GameObject gameObject)
		{
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

				if (gameOjectTag == Config.DRONE_TAG) {
						deadPlayer = gameOjectTag;
						winner = "Refugee";
				} else if (gameOjectTag == Config.REFUGEE_TAG) {
						deadPlayer = gameOjectTag;
						winner = "Drone";
				}
		}

		[RPC]
		public void ShowRefugeeTrace ()
		{   
				showRefugeeTrace = true;  
				GameObject refugee = GameObject.FindGameObjectWithTag (Config.REFUGEE_TAG);

		}

		private void showRefugeeTraceWhileLeftEyeClosed ()
		{
				GameObject refugee = GameObject.FindGameObjectWithTag (Config.REFUGEE_TAG);
				if (gazeModel.diamLeftEye == 0) {
						refugee.transform.FindChild ("TraceLight").gameObject.SetActive (true);
						Debug.Log ("sRTELEC if");

				} else {
						refugee.transform.FindChild ("TraceLight").gameObject.SetActive (false);
						showRefugeeTrace = false;
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

		[RPC]
		public void QuitGame (string situation)
		{
				Application.LoadLevel (situation);
		}

		void OnGUI ()
		{
				if (gameTime > 0) {
						GUI.Box (gameTimeGUIPosition, "Zeit: " + gameTimeString);

				} else {
						gameTimeString = "0:00";
						GUI.Box (gameTimeGUIPosition, "Zeit: " + gameTimeString);
						GUI.Label (new Rect (10, 10, 300, 20), "Time is over! Drone won the game, Refugee lost!");
						deadPlayer = "Refugee";
						networkView.RPC ("QuitGame", RPCMode.All, "DroneWon");
				}

				if (showMenu) {
						if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 50), "B: Beenden")) {
								if (Network.isClient)
										deadPlayer = "Refugee";
								if (Network.isServer)
										deadPlayer = "Drone";
						}
						if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "F: Fortsetzen")) {
								showMenu = !showMenu;
						}
            
				}
		}
}
