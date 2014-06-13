using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    CharacterController characterController;

    public float rotationSpeed = 100;
    public float walkingSpeed = 10;

    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;

    public ThirdPersonCameraController mainGameCamera;
    private float directionSpeed = 3.0f;
    private float directionDampTime = .25f;
    private float rotationDegreePerSecond = 120f;

    private float vertical = 0.0f;
    private float horizontal = 0.0f;
    private float strafe = 0.0f;
    private float speed = 0.0f;
    private float direction = 0.0f;
    private float scopeCounter = 0;




    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
      
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        strafe = Input.GetAxis("Strafe");
       
        ControlsToWorldSpace(this.transform, mainGameCamera.transform, ref direction, ref speed);

        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        Vector3 dir = this.transform.right * strafe + this.transform.forward * vertical;
        characterController.SimpleMove(dir * this.walkingSpeed);

    }

    void FixedUpdate()
    {

        if((directionSpeed >= 0 && horizontal >= 0) || (directionSpeed < 0 && horizontal < 0))
        {
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            this.transform.rotation = (this.transform.rotation * deltaRotation);
        }

    }

    public void ControlsToWorldSpace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 controlsDirection = new Vector3(horizontal, 0, vertical);

        speedOut = controlsDirection.sqrMagnitude;

        // get camera rotation

        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

        // controls input in worldspace coords

        Vector3 moveDirection = referentialShift * controlsDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
    }





    void SpeedUp()
    {
        Debug.Log("in speedup");
        this.walkingSpeed *= 5;
    }

    void PickedUpScope()
    {

        scopeCounter++;

        if (scopeCounter % 2 == 0)
        {
            thirdPersonCamera.enabled = true;
            firstPersonCamera.enabled = false;
        }
        else
        {
            thirdPersonCamera.enabled = false;
            firstPersonCamera.enabled = true;

        }
    }

	
}
