using UnityEngine;
using System.Collections;

public class TeleportBehaviour : MonoBehaviour
{

    private float gazeSpeed = 1f;
    private bool showNoWeaponText = false;
    private bool inShooting = false;
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
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;
    }

    private void reactOnDisableShoot()
    {
        inShooting = false;
        showNoWeaponText = false;
    }

    private void reactOnEnableShoot()
    {
        inShooting = true;
    }

    void OnGUI()
    {
        if (showNoWeaponText)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 400f, 200f), "Mit gezückter Waffe betritt man keinen Teleporter!");
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (onTeleporter && !inShooting)
        {

            GameeventManager.onTeleporterField();
            /*float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
            xAxisWithLimit += inputXAxis * 1f;

            float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
            yAxisWithLimit += inputYAxis * 1f; ;
            */
            refugee.transform.FindChild("Main Camera").gameObject.SetActive(false);
            refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(true);
            camera = refugee.transform.FindChild("TopDownCamera").camera;
            camera.transform.position = new Vector3(camera.transform.position.x + gazeInput.getCameraXPositionWithGaze() * gazeSpeed, camera.transform.position.y, camera.transform.position.z + gazeInput.getCameraYPositionWithGaze() * gazeSpeed);
            lookForTeleportPosition();
            teleport();
        }

    }



    private float cameraPositionFromGaze()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        float speed = 0;

        //Top
        if (AOI_Top.volume.Contains(actualEyePosition))
        {
            speed++;
        }

        return speed;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Tags.REFUGEE)
        {
            if (inShooting)
            {
                showNoWeaponText = true;
            }
            else
            {
                refugee = GameObject.FindGameObjectWithTag("Refugee").gameObject;
                gazeInput = refugee.gameObject.GetComponent<GazeInputFromAOI>();
                //refugee.transform.FindChild("Main Camera").camera.enabled = false;
                //refugee.transform.FindChild("TopDownCamera").camera.enabled = true;
                //camera = refugee.transform.FindChild("Main Camera").camera;

                refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(true);
                camera = refugee.transform.FindChild("TopDownCamera").gameObject.camera;
                camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                onTeleporter = true;
                calculateAOI();
            }
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
        if (inShooting)
        {

        }
        else
        {
            GameeventManager.onTeleportLeft();
            refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(false);
            refugee.transform.FindChild("Main Camera").gameObject.SetActive(true);
        }
        if (collider.gameObject.tag == Tags.REFUGEE)
        {
            onTeleporter = false;
        }

    }

    private void lookForTeleportPosition()
    {
        RaycastHit hit;

        Vector3 refugeeTeleportPositionFrom = refugee.transform.position;

        Ray ray = camera.ScreenPointToRay(camera.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 teleportPosition = camera.transform.position;


        }

        refugeeTeleportPositionTo = new Vector3(camera.transform.position.x, 2f, camera.transform.position.z);

    }

    private void teleport()
    {

        if (Input.GetButtonUp("Teleport"))
        {
            Debug.Log("refugee" + refugee);
            Debug.Log("position: " + refugeeTeleportPositionTo);
            refugee.transform.position = refugeeTeleportPositionTo;
            refugee.transform.FindChild("Main Camera").gameObject.SetActive(true);
            refugee.transform.FindChild("TopDownCamera").gameObject.SetActive(false);
            camera = refugee.transform.FindChild("Main Camera").camera;

        }
    }

    private Vector3 getRefugeeTeleportPositionTo()
    {
        Vector3 actualEyePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        return actualEyePosition;
        // return new Vector3((gazeModel.posGazeLeft.x + gazeModel.posGazeRight.x) * 0.5f, (gazeModel.posGazeLeft.y + gazeModel.posGazeRight.y) * 0.5f, 0f);

    }



}
