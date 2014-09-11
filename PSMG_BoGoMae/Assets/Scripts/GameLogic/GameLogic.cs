using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

		private RefugeeMovement refugeeMovement;
		private int transmitterCounter;

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
				Debug.Log (gameObject.name);
				networkView.RPC ("PlayerDied", RPCMode.All, gameObject);
		}

		[RPC]
		public void PlayerDied (GameObject gameObject)
		{
				//Debug.Log (" isClient: " + Network.isClient + " isServer: " + Network.isServer);
				if (gameObject.tag == Config.DRONE_TAG) {
					Debug.Log("Drone died");
				} else if (gameObject.tag == Config.REFUGEE_TAG) {
					Debug.Log("Refugee died");
				}
		}
}
