using UnityEngine;
using System.Collections;

public class TeleportBehaviour : MonoBehaviour
{

    private int activateEyeTracker = 1;
    private int deactivateEyeTracker = 2;
    private bool onTeleporter = false;
    private Rect gazeTexturePosition = new Rect(0f, 0f, Screen.width, 250f);
    public Texture2D crosshair;
    public Texture2D gazeTexture;
    private Vector3 refugeeTeleportPositionTo;
    private GameObject refugee;
    private Camera camera;
    private float xAxisWithLimit;
    private float yAxisWithLimit;
    private GazeInputFromAOI gazeInput;
    private float widthAOI;
    private float heightAOI;
    private AOI AOI_Top;


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
            /*float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
            xAxisWithLimit += inputXAxis * 1f;

            float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
            yAxisWithLimit += inputYAxis * 1f; ;
            */
            camera.transform.position = new Vector3(camera.transform.position.x +gazeInput.getCameraXPositionWithGaze(), camera.transform.position.y, camera.transform.position.z + gazeInput.getCameraYPositionWithGaze());
            lookForTeleportPosition();
            teleport();
        }
    }

    void OnGUI()
    {
        if (onTeleporter)
        {
           // GUI.DrawTexture(gazeTexturePosition, gazeTexture);
        }
    }

    private float cameraPositionFromGaze()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        float speed = 0;

        //Top
        if (AOI_Top.volume.Contains(actualEyePosition))
        {
            speed ++;
        }

        return speed;
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
            calculateAOI();
        }
        else
        {
            onTeleporter = false;
        }
    }

    private void calculateAOI()
    {
        Rect topVolume = new Rect(0, 0, Screen.width, 250f);
        Vector3 topStart = Vector3.zero;
        Vector3 topEnd = new Vector3(Screen.width, 0, 0);
        AOI_Top = new AOI(topVolume, topStart, topEnd);
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
