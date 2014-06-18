using UnityEngine;
using System.Collections;

public class DroneCameraScript : MonoBehaviour {

    public Transform drone;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = drone.position;
	}
}
