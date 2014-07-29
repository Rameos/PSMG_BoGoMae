using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public Transform drone;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 pos = drone.position;
        pos.y = 0;
        transform.position = pos;
        
	}
}
