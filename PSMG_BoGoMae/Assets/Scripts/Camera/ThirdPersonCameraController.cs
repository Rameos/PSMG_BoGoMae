using UnityEngine;
using System.Collections;


public class ThirdPersonCameraController : MonoBehaviour {

    [SerializeField]
    private float cameraDistance = 25;
    [SerializeField]
    private float cameraDistanceHeight = 4.5f;
    [SerializeField]
    private float smooth = 2;
    [SerializeField]
    private Transform targetToFollow;
    [SerializeField]
    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);
    [SerializeField]
    private float widescreen = 0.2f;
    [SerializeField]
    private float firstPersonThreshold = 0.5f;
    [SerializeField]
    private float firstPersonLookSpeed = 3.0f;
    [SerializeField]
    private float firstPersonDegreePerSecond = 120.0f;
    [SerializeField]
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 60.0f);

    private Vector3 targetPosition;
    private Vector3 lookDirection;

    // smoothing and damping
    private Vector3 velocityCameraSmooth = Vector3.zero;
    [SerializeField]
    private float cameraSmoothDampTime = 0.1f;
    private CameraStates cameraState = CameraStates.Behind;
    private const float CAMERA_RESET_TRESHOLD = 0.1f;

    public CameraStates CameraState
    {
        get { return cameraState; }
        set { cameraState = value; }
    }

    private float xAxisRotation = 0.0f;
    private float lookWeight;
    private bool inFirstPersonView = false;
 

    public enum CameraStates
    {
        Behind,
        ResetBehind,
    }



	// Use this for initialization
	void Start () {

        targetToFollow = GameObject.FindWithTag("Player").transform;
        lookDirection = targetToFollow.forward;

         
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {

    }



    void LateUpdate()
    {

            Vector3 playerOffset = targetToFollow.position + new Vector3(0f, cameraDistanceHeight, 0f);
            Vector3 lookAt = playerOffset;

            /*
             *      reset camera state
             */
            if (Input.GetAxis("ThirdPersonCameraReset") > CAMERA_RESET_TRESHOLD)
            {
                cameraState = CameraStates.ResetBehind;
            }
            else
            {

                /*
                 *      default camera state
                 */
                if ((cameraState == CameraStates.ResetBehind && (Input.GetAxis("ThirdPersonCameraReset") <= CAMERA_RESET_TRESHOLD)))
                {
                    cameraState = CameraStates.Behind;
                }

            }

            switch (cameraState)
            {

                case CameraStates.Behind:
                    ResetCamera();
                    // calc direction from camera to player
                    lookDirection = playerOffset - this.transform.position;
                    lookDirection.y = 0;
                    lookDirection.Normalize();
                    targetPosition = playerOffset + targetToFollow.up * cameraDistanceHeight - lookDirection * cameraDistance;

                    break;


                case CameraStates.ResetBehind:
                    ResetCamera();
                    lookDirection = targetToFollow.forward;

                    break;

                default:
                    break;

            }

            targetPosition = playerOffset + targetToFollow.up * cameraDistanceHeight - lookDirection * cameraDistance;
            smoothPosition(this.transform.position, targetPosition);

            transform.LookAt(lookAt);
        
    }

    private void smoothPosition(Vector3 fromPosition, Vector3 toPosition)
    {
        // a smooth transition from current to wanted camera position
        this.transform.position = Vector3.SmoothDamp(fromPosition, toPosition, ref velocityCameraSmooth, cameraSmoothDampTime);
    }

    private void ResetCamera()
    {
        lookWeight = Mathf.Lerp(lookWeight, 0.0f, Time.deltaTime * firstPersonLookSpeed);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
    }
}
