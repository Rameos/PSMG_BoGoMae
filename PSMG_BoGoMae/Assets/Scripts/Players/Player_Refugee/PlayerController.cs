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
    private float jump = 0.0f;
    private float strafe = 0.0f;
    private float speed = 0.0f;
    private float direction = 0.0f;
    private float scopeCounter = 0;

    private Animator anim;
    public int runningBool;
    private int runningBackwardsBool;
    private int jumpingBool;

    int turnLeft = Animator.StringToHash("TurnLeft");
    int turnRight = Animator.StringToHash("TurnRight");

    void Awake()
    {
        anim = GetComponent<Animator>();
        //anim.SetLayerWeight(1, 1f);
        runningBool = Animator.StringToHash("running");
        jumpingBool = Animator.StringToHash("jumping");
       // runningBackwardsBool = Animator.StringToHash("runningBackwards");

    }

    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        GameeventManager.useScopeHandler += reactOnUseScope;
        GameeventManager.useSpeedHandler += reactOnUseSpeed;      
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        strafe = Input.GetAxis("Strafe");
        jump = Input.GetAxis("Jump");
        
       
        //ControlsToWorldSpace(this.transform, mainGameCamera.transform, ref direction, ref speed);

        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);

        if (jump > 0)
        {
            anim.SetBool(jumpingBool, true);
            //yield return new WaitForFixedUpdate();
        }
        else
        {
           // if (!animation.IsPlaying("Jump"))
            anim.SetBool(jumpingBool, false);
        }
        //////////////////////////////////
        if (horizontal < 0)
        {
            anim.SetBool(turnLeft, true);
            // characterController.SimpleMove(dir * this.walkingSpeed);
        }
        else if (horizontal > 0)
        {
            // characterController.SimpleMove(dir * this.walkingSpeed);
               anim.SetBool(turnRight, true);
        }
        else
        {
            anim.SetBool(turnLeft, false);
            anim.SetBool(turnRight, false);

        }
        ///////////////////////////////////
        
        if (vertical > 0 )
        {
            anim.SetBool(runningBool, true);
           // characterController.SimpleMove(dir * this.walkingSpeed);
        }
        else if (vertical < 0)
        {
           // characterController.SimpleMove(dir * this.walkingSpeed);
         //   anim.SetBool(runningBackwardsBool, true);
        }
        else
        {
            anim.SetBool(runningBool, false);
          //  anim.SetBool(runningBackwardsBool, false);
        }

    }

    void FixedUpdate()
    {

        if((directionSpeed >= 0 && horizontal >= 0) || (directionSpeed < 0 && horizontal < 0))
        {
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
            Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
            //this.transform.rotation = (this.transform.rotation * deltaRotation);
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


    // receive message from PlayerCollisionResponse
    void reactOnUseSpeed()
    {
        this.walkingSpeed *= 5;
    }

    void controlMovement()
    {

    }

    // receive message from PlayerCollisionResponse
    private void reactOnUseScope()
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
