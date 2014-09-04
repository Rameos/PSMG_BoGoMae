using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

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
    private float xAxisMin = -30;
    private float xAxisMax = 30;
    private float yAxisMin = -360;
    private float yAxisMax = 360;
    private GazeInputFromAOI gazeInput;
    private CameraStates cameraState = CameraStates.FirstPerson;
    private float upDownLookRange = 60f;
    private float firstPersonLookSpeed = 1f;

    public enum CameraStates
    {
        FirstPerson,
        Shooting,
        LookAround

    }

	// Use this for initialization
	void Start () {
        //Screen.lockCursor = true;
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        GameeventManager.onLookAroundClickedHandler += reactOnEnableFirstPersonCamera;
        GameeventManager.onShootClickedHandler += reactOnEnableShoot;

	}


	
	// Update is called once per frame
	void Update () {

            determineCameraState();

            if (Input.GetButton("Jump"))
            {
                verticalVelocity = jumpSpeed;
            }

        

	}

    void LateUpdate()
    {

            switch (cameraState)
            {

                case CameraStates.FirstPerson:

                    rotateCamWithMouse();

                    break;

                case CameraStates.Shooting:

                    rotateCamWithMouse();

                    break;

                case CameraStates.LookAround:

                    rotateCamWithGaze();

                    break;

            }
        
    }
    /*
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        Camera camera = GameObject.FindGameObjectWithTag("RefugeeCamera").camera;
        if (networkView.isMine)
        {
            camera.enabled = true;
        }
        else
        {
            camera.enabled = false;
        }
    }
     */
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
        else
        {
            cameraState = CameraStates.FirstPerson;
        }
    }
    private void reactOnEnableShoot(int counter)
    {
        inShooting = true;
    }

    private void reactOnEnableFirstPersonCamera(int counter)
    {
        inLookAround = true;
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
    }
}
