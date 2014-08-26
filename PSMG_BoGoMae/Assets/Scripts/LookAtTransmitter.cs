using UnityEngine;
using System.Collections;
using iViewX;

public class LookAtTransmitter : MonoBehaviourWithGazeComponent {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override void OnGazeEnter(RaycastHit hit)
    {
        //Debug.Log("gaze on transmitter");
    }
    public override void OnGazeStay(RaycastHit hit)
    {
       // Debug.Log("gaze stays on transmitter");

        if (Input.GetButton("DeactivateTransmitter"))
        {
            Destroy(gameObject);
        }
    }
    public override void OnGazeExit()
    {
       
    }


}
