using UnityEngine;
using System.Collections;

public class TeleportBehaviour : MonoBehaviour {

    private int activateEyeTracker = 1;
    private int deactivateEyeTracker = 2;
    private bool onTeleporter = false;
    private Vector3 refugeeTeleportPositionTo;
    private GameObject refugee;


	// Use this for initialization
	void Start () {
        refugee = GameObject.FindGameObjectWithTag(Tags.REFUGEE);
	}

	// Update is called once per frame
	void Update () {
        if (onTeleporter)
        {
            lookForTeleportPosition();
            teleport();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == Tags.REFUGEE)
        {
            onTeleporter = true;

        }
        else
        {
            onTeleporter = false;
        }
    }

    private void lookForTeleportPosition()
    {
        RaycastHit hit;
        GameeventManager.onLookAroundClicked(activateEyeTracker);

        Vector3 refugeeTeleportPositionFrom = refugee.transform.position;
       
        Ray ray = Camera.main.ScreenPointToRay(getRefugeeTeleportPositionTo());
        if (Physics.Raycast(ray, out hit))
        {
            refugeeTeleportPositionTo = hit.transform.position;
        }
        
    }

    private void teleport()
    {
        if (Input.GetButton("Teleport"))
        {
            refugee.transform.position = refugeeTeleportPositionTo;
            GameeventManager.onLookAroundClicked(deactivateEyeTracker);
        }
    }

    private Vector3 getRefugeeTeleportPositionTo()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        return actualEyePosition;
       // return new Vector3((gazeModel.posGazeLeft.x + gazeModel.posGazeRight.x) * 0.5f, (gazeModel.posGazeLeft.y + gazeModel.posGazeRight.y) * 0.5f, 0f);

    }
	


}
