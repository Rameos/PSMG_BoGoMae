using UnityEngine;
using System.Collections;

public class RefugeeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float leftRightMovement = Input.GetAxis("Horizontal");
        float forwardBackwardMovement = Input.GetAxis("Vertical");

        transform.rigidbody.AddForce(leftRightMovement, 0, forwardBackwardMovement, ForceMode.Acceleration);
	}
}
