using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.N)) {
						RestartGame ();

				}
		}

		private void RestartGame ()
		{

				Network.Disconnect ();
				MasterServer.UnregisterHost ();

				if (Network.isClient) {
						Network.Destroy (gameObject);
				}

				Application.LoadLevel ("Final_Scene");
		}

		void OnGUI ()
		{
				if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "N: Neu Starten")) {
						RestartGame ();
				}
		}
}
