using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 5.0f;
    public float jumpSpeed = 50.0f;

    private bool inLookAround = false;
    private bool inShooting = false;

    float verticalRotation = 0;
    private float rotationUpDown;
    private float rotationLeftRight;
    public float upDownRange = 60.0f;

    float verticalVelocity = 0;
    public Camera camera;

    private float xAxisWithLimit;
    private float yAxisWithLimit;
    private float xAxisMin = -80;
    private float xAxisMax = 80;
    private float yAxisMin = -360;
    private float yAxisMax = 360;
    private GazeInputFromAOI gazeInput;
    private CameraStates cameraState = CameraStates.FirstPerson;
    private float upDownLookRange = 80f;
    private float firstPersonLookSpeed = 1f;
    private bool onTeleportField = false;
    private bool setTeleportCamOnce = false;
    private Vector3 defaultCameraPosition;
    private bool active = false;
    private bool inactive = true;

    public static bool soundEnabled = false;

    public enum CameraStates
    {
        FirstPerson,
        Shooting,
        LookAround,
        onTeleport

    }

    // Use this for initialization
    void Start()
    {
        //Screen.lockCursor = true;
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        GameeventManager.onLookAroundClickedHandler += reactOnEnableFirstPersonCamera;
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;
        GameeventManager.onTeleporterFieldHandler += reactOnTeleportField;
        GameeventManager.onTeleportLeftHandler += reactOnTeleportLeft;
    }


    // Update is called once per frame
    void Update()
    {

        determineCameraState();

        if (Input.GetButton("Jump"))
        {
            verticalVelocity = jumpSpeed;
        }

        if (Input.GetButtonDown("Sound"))
        {
            reactOnEnableSound();

        }

    }

    void OnGUI()
    {
        if (soundEnabled)
        {
            GUI.Box(new Rect(100, 10, 100, 50), "Sound an");
        }
    }

    void LateUpdate()
    {

        switch (cameraState)
        {

            case CameraStates.FirstPerson:
                rotateCamWithMouse();
                setXRay(inactive);

                //defaultCameraPosition = camera.transform.position;
                break;

            case CameraStates.Shooting:
                rotateCamWithMouse();
                setXRay(inactive);

                break;

            case CameraStates.LookAround:
                rotateCamWithGaze();
                if (gazeModel.diamLeftEye == 0)
                {
                    setXRay(active);
                }
                else
                {
                    setXRay(inactive);
                }
                break;

            case CameraStates.onTeleport:

                setXRay(inactive);

                break;

        }

    }

    private void setXRay(bool state)
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag(Config.BUILDINGS_TAG);
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].gameObject.renderer.enabled = state;
        }
    }


    public void reactOnEnableSound()
    {
        soundEnabled = !soundEnabled;
        Debug.Log("Sound: " + soundEnabled.ToString());
        playSoundIfEnabled();

    }

    private void playSoundIfEnabled()
    {
        AudioSource sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        if (soundEnabled)
        {
            sound.audio.Play();
            Debug.Log(sound);
        }
        else
        {
            sound.Stop();
        }
    }

    private void determineCameraState()
    {

        if (inLookAround)
        {

            cameraState = CameraStates.LookAround;
        }
        else if (inShooting)
        {

            cameraState = CameraStates.Shooting;
        }
        else if (onTeleportField)
        {

            cameraState = CameraStates.onTeleport;
        }
        else
        {

            cameraState = CameraStates.FirstPerson;
        }
    }


    private void reactOnTeleportField()
    {

        onTeleportField = true;
        if (setTeleportCamOnce)
        {

        }
        else
        {
            //setCameraTopDownView();
            setTeleportCamOnce = true;
        }

    }

    private void reactOnTeleportLeft()
    {
        onTeleportField = false;
        //setCameraFirstPersonView();
    }

    private void reactOnEnableShoot()
    {
        inShooting = true;
    }


    private void reactOnDisableShoot()
    {
        inShooting = false;
    }

    private void reactOnEnableFirstPersonCamera(int counter)
    {
        if (counter % 2 == 0)
        {
            inLookAround = false;
        }
        else
        {
            inLookAround = true;
        }
    }

    private void setCameraTopDownView()
    {
        //transform.FindChild("Main Camera").camera.enabled = false;
        //transform.FindChild("TopDownCamera").camera.enabled = true;
        camera.transform.position = new Vector3(0f, 850f, 0f);
        camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void setCameraFirstPersonView()
    {
        Debug.Log("determineCameraState: onteleportfield: " + onTeleportField);
        //transform.FindChild("Main Camera").camera.enabled = true;
        //camera = transform.FindChild("Main Camera").camera;
        //transform.FindChild("TopDownCamera").camera.enabled = false;
        camera.transform.position = new Vector3(0f, 2f, 0.42f);
    }

    private void rotateCamWithMouse()
    {
        rotationLeftRight += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationUpDown = Mathf.Clamp(rotationUpDown, -upDownLookRange, upDownLookRange);
        camera.transform.rotation = Quaternion.Euler(rotationUpDown, rotationLeftRight, 0);
        transform.rotation = Quaternion.Euler(0, rotationLeftRight, 0);
    }

    private void rotateCamWithGaze()
    {
        Debug.Log("in Cam with gaze");
        float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
        xAxisWithLimit += inputXAxis * firstPersonLookSpeed;
        //Debug.Log(inputXAxis + " || " + xAxisWithLimit);
        float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
        yAxisWithLimit += inputYAxis * firstPersonLookSpeed;

        xAxisWithLimit = Mathf.Clamp(xAxisWithLimit, xAxisMin, xAxisMax);
        yAxisWithLimit = Mathf.Clamp(yAxisWithLimit, yAxisMin, yAxisMax);

        camera.transform.rotation = Quaternion.Euler(xAxisWithLimit, yAxisWithLimit, 0);
        transform.rotation = Quaternion.Euler(0, yAxisWithLimit, 0);
    }


}