using UnityEngine;
using System.Collections;

public class TeleportBehaviour : MonoBehaviour
{

    private int activateEyeTracker = 1;
    private int deactivateEyeTracker = 2;
    private bool onTeleporter = false;
    private float crosshairWidth = 32;
    private float crosshairHeight = 32;
    public Texture2D crosshair;
    private Vector3 refugeeTeleportPositionTo;
    private GameObject refugee;
    private Camera camera;
    private float xAxisWithLimit;
    private float yAxisWithLimit;
    private GazeInputFromAOI gazeInput;


    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (onTeleporter)
        {

            GameeventManager.onTeleporterField();
            float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
            xAxisWithLimit += inputXAxis * 2f;

            float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
            yAxisWithLimit += inputYAxis * 2f; ;
            camera.transform.position = new Vector3(xAxisWithLimit, camera.transform.position.y, yAxisWithLimit);
            lookForTeleportPosition();
            teleport();
        }
    }




    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Tags.REFUGEE)
        {
            refugee = GameObject.FindGameObjectWithTag("Refugee").gameObject;
            gazeInput = refugee.gameObject.GetComponent<GazeInputFromAOI>();
            //refugee.transform.FindChild("Main Camera").camera.enabled = false;
            //refugee.transform.FindChild("TopDownCamera").camera.enabled = true;
            camera = refugee.transform.FindChild("Main Camera").camera;

            onTeleporter = true;
        }
        else
        {
            onTeleporter = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == Tags.REFUGEE)
        {
            onTeleporter = false;
        }

    }

    private void lookForTeleportPosition()
    {
        RaycastHit hit;

        Vector3 refugeeTeleportPositionFrom = refugee.transform.position;

        /*Ray ray = Camera.main.ScreenPointToRay(camera.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            refugeeTeleportPositionTo = new Vector3(hit.transform.position.x, 2f, hit.transform.position.z);
        }*/

        refugeeTeleportPositionTo = new Vector3(camera.transform.position.x, 2f, camera.transform.position.z);

    }

    private void teleport()
    {

        if (Input.GetButtonUp("Teleport"))
        {
            Debug.Log("refugee" + refugee);
            Debug.Log("position: " + refugeeTeleportPositionTo);
            refugee.transform.position = refugeeTeleportPositionTo;
            GameeventManager.onTeleportLeft();
        }
    }

    private Vector3 getRefugeeTeleportPositionTo()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        return actualEyePosition;
        // return new Vector3((gazeModel.posGazeLeft.x + gazeModel.posGazeRight.x) * 0.5f, (gazeModel.posGazeLeft.y + gazeModel.posGazeRight.y) * 0.5f, 0f);

    }



}
