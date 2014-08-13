using UnityEngine;
using System.Collections;

public class DroneCameraScript : MonoBehaviour {

    public Transform drone;


	// Use this for initialization
	void Start () {
        GameeventManager.droneIsActiveHandler += reactOnDroneIsActive;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindWithTag("Drone") != null)
        {
            transform.position = drone.position;

        }
	}

    private void reactOnDroneIsActive()
    {
        // aktuell unnötig, event kann man aber bestimmt trotzdem brauchen später
    }
}
