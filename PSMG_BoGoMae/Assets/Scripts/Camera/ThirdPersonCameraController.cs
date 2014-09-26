using UnityEngine;
using System.Collections;


public class ThirdPersonCameraController : MonoBehaviour {

    /*
     * first person camera variables
     */

    private Vector2 gazePos;
    private float xAxisRotation;
    private float yAxisRotation;
    private float xAxisWithLimit;
    private float yAxisWithLimit;
    private float xAxisMin = -30;
    private float xAxisMax = 30;
    private float yAxisMin = -360;
    private float yAxisMax = 360;
    public Texture2D crosshair;
    private float crosshairWidth = 32;
    private float crosshairHeight = 32;

    private float rotationY = 0f;
    private float firstPersonLookSpeed = 0.2f;
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 60.0f);
    private float firstPersonRotationDegreePerSecond = 120f;
    GazeInputFromAOI gazeInput;
    [SerializeField]
    private float rotationSpeed = 2f;
    private float mouseSensitivity = 5.0f;

    /*
     * third person camera variables
     */
    public float cameraDistanceAwayToPlayer;
    public float cameraDistanceUpToPlayer;

    public Vector3 lookDirection;
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);

    private Transform targetToFollow;
    private Vector3 targetPosition;
    private Vector3 velocityCamSmooth = Vector3.zero;
    private float camSmoothDampTime = 0.1f;
    private CameraStates cameraState = CameraStates.Behind;
    private bool inLookAround = false;
    private bool inShooting = false;
    private float rotationUpDown;
    private float rotationLeftRight;
    private float upDownLookRange = 60f;

    public enum CameraStates
    {
        Behind,
        FirstPerson,
        Shooting,
        Reset
    }

    void Start()
    {

        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        targetToFollow = GameObject.FindGameObjectWithTag("TargetToFollow").transform;
        GameeventManager.onLookAroundClickedHandler += reactOnEnableFirstPersonCamera;
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
    }



    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 characterOffset = targetToFollow.position + new Vector3(0f, cameraDistanceUpToPlayer, 0f);

        determineCameraState();

        switch (cameraState)
        {
            case CameraStates.Behind:

                lookDirection = characterOffset - this.transform.position;
                lookDirection.y = 0;
                lookDirection.Normalize();
                targetPosition = characterOffset + targetToFollow.up * cameraDistanceUpToPlayer - lookDirection * cameraDistanceAwayToPlayer;
         
            break;

            case CameraStates.Reset:

                lookDirection = targetToFollow.forward;
                
            break;

            case CameraStates.FirstPerson:

                transform.position = targetToFollow.position;
                rotateCamWithGaze();

            break;

            case CameraStates.Shooting:

                transform.position = targetToFollow.position;
                rotateCamWithMouse();

            break;

        }


        if (!inLookAround && !inShooting)
        {
            targetPosition = characterOffset + targetToFollow.up * cameraDistanceUpToPlayer - lookDirection * cameraDistanceAwayToPlayer;

            compensateWalls(characterOffset, ref targetPosition);

            smoothPosition(this.transform.position, targetPosition);
        
            transform.LookAt(targetToFollow);

        }
    }


    private void determineCameraState()
    {
        Debug.Log(inLookAround);
        if (Input.GetButton("ResetCamera"))
        {
            cameraState = CameraStates.Reset;
        }
        else if (inLookAround)
        {
            cameraState = CameraStates.FirstPerson;
        }
        else if (inShooting)
        {
            cameraState = CameraStates.Shooting;
        }
        else
        {
            cameraState = CameraStates.Behind;
        }
    }

    private void smoothPosition(Vector3 fromPosition, Vector3 toPosition)
    {
        this.transform.position = Vector3.SmoothDamp(fromPosition, toPosition, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void compensateWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
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


    private void reactOnEnableShoot()
    {

         inShooting = true;
       
    }

    void OnGUI()
    {

        if (inLookAround)
        {
            GUI.DrawTexture(new Rect(getCrosshairXPosition(), getCrosshairYPosition(), crosshairWidth, crosshairHeight), crosshair);
        }
    }

    public float getCrosshairXPosition()
    {

        return (gazeModel.posGazeLeft.x + gazeModel.posGazeRight.x) * 0.5f;
    }

    public float getCrosshairYPosition()
    {
        return (gazeModel.posGazeLeft.y + gazeModel.posGazeRight.y) * 0.5f;
    }



    private void rotateCamWithGaze()
    {
        float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
        xAxisWithLimit += inputXAxis * firstPersonLookSpeed;

        float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
        yAxisWithLimit += inputYAxis * firstPersonLookSpeed;
        xAxisWithLimit = Mathf.Clamp(xAxisWithLimit, xAxisMin, xAxisMax);
        yAxisWithLimit = Mathf.Clamp(yAxisWithLimit, yAxisMin, yAxisMax);

        transform.rotation = Quaternion.Euler(xAxisWithLimit, yAxisWithLimit, 0);
    }


    private void rotateCamWithMouse()
    {
        rotationLeftRight += Input.GetAxis("Mouse X") * mouseSensitivity;     
        rotationUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationUpDown = Mathf.Clamp(rotationUpDown, -upDownLookRange, upDownLookRange);
        transform.rotation = Quaternion.Euler(rotationUpDown, rotationLeftRight, 0);
        this.transform.rotation = Quaternion.Euler(0, rotationLeftRight, 0);

    }
}
