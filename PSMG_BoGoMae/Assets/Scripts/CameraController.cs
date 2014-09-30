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
    public Texture2D sound_on;
    public Texture2D sound_off;

    public static bool soundEnabled = true;

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
            GUI.DrawTexture(new Rect(50, 10, 50, 50), sound_on);
        }
        else
        {
            GUI.DrawTexture(new Rect(50, 10, 50, 50), sound_off);
        }
    }

    void LateUpdate()
    {

        switch (cameraState)
        {

            case CameraStates.FirstPerson:
                rotateCamWithMouse();
                setXRay(inactive);
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
        playSoundIfEnabled();

    }

    public void playSoundIfEnabled()
    {
        AudioSource sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        if (soundEnabled)
        {
            sound.audio.Play();
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
            setTeleportCamOnce = true;
        }

    }

    private void reactOnTeleportLeft()
    {
        onTeleportField = false;
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
        camera.transform.position = new Vector3(0f, 850f, 0f);
        camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void setCameraFirstPersonView()
    {
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
        float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
        xAxisWithLimit += inputXAxis * firstPersonLookSpeed;
        float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
        yAxisWithLimit += inputYAxis * firstPersonLookSpeed;

        xAxisWithLimit = Mathf.Clamp(xAxisWithLimit, xAxisMin, xAxisMax);
        yAxisWithLimit = Mathf.Clamp(yAxisWithLimit, yAxisMin, yAxisMax);

        camera.transform.rotation = Quaternion.Euler(xAxisWithLimit, yAxisWithLimit, 0);
        transform.rotation = Quaternion.Euler(0, yAxisWithLimit, 0);
    }


}