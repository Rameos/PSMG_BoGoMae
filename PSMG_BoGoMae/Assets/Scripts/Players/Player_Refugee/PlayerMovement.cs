using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    private Animator animator;
    private HashIDs hashIDs;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hashIDs = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<HashIDs>();
        //animator.SetLayerWeight(1, 1f);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        MovementManagment(horizontal, vertical);
        
    }

    void MovementManagment(float horizontal, float vertical)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            Rotating(horizontal, vertical);
            animator.SetFloat(hashIDs.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(hashIDs.speedFloat, 0f);
        }
    }

    void Rotating(float horizontal, float vertical) 
    {
        Vector3 targetDirection = GetTargetDirectionFromInput(horizontal, vertical);
        Quaternion targetRotation = GetTargetRotationFromNewDirection(ref targetDirection);
        Quaternion newRotation = GetNewRotationFromTarget(targetRotation);
        rigidbody.MoveRotation(newRotation);    

    }

    private Quaternion GetNewRotationFromTarget(Quaternion targetRotation)
    {
        return Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }

    private static Quaternion GetTargetRotationFromNewDirection(ref Vector3 targetDirection)
    {
        return Quaternion.LookRotation(targetDirection, Vector3.up);
    }

    private Vector3 GetTargetDirectionFromInput(float horizontal, float vertical)
    {
        return new Vector3(horizontal, 0f, vertical);
    }
}
