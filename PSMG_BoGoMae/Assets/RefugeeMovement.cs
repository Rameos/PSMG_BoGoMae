using UnityEngine;
using System.Collections;

public class RefugeeMovement : MonoBehaviour
{

    Animator animator;
    private CharacterController characterController;
    private float movementSpeed = 50f;
    private float movementSpeedDefault = 50f;
    private float movementSpeedWithItem = 150f;
    private float speedItemDuration = 10f;
    private bool speedItemIsUsed = false;
    private Rect speedItemDurationGUIposition = new Rect(400f, 50f, 150f, 50f);
    private bool onTeleport = false;
    private Vector3 curVel = Vector3.zero;
    private float friction = 0.4f;

    // Use this for initialization
    void Start()
    {
        GameeventManager.useSpeedHandler += reactOnUseSpeedItem;
        GameeventManager.onTeleporterFieldHandler += reactOnTeleportField;
        GameeventManager.onTeleportLeftHandler += reactOnTeleportLeft;

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }





    // Update is called once per frame
    void Update()
    {
        MovementFromInput();
        if (speedItemIsUsed)
        {
            HandleSpeedItem();

        }
        /*if (onTeleport)
        {
            animator.SetFloat("Forward", 0f);
            //animator.CrossFade("idle");
        }
        else
        {
            //animator.speed = 1f;
            Debug.Log("in react on teleport left, animator.speed: " + animator.speed);
            MovementFromInput();
            HandleSpeedItem();
        }*/
    }



    void OnGUI()
    {
        if (speedItemIsUsed && speedItemDuration > 0)
        {
            GUI.Box(speedItemDurationGUIposition, "Speed-Boost Zeit: " + speedItemDuration.ToString("0"));
        }
    }

    private void HandleSpeedItem()
    {
        CheckSpeedItemDuration();
        if (speedItemIsUsed && speedItemDuration > 0)
        {
            movementSpeed = movementSpeedWithItem;
        }
        else
        {
            movementSpeed = movementSpeedDefault;
        }
    }

    private void CheckSpeedItemDuration()
    {
        if (speedItemDuration >= 0)
        {
            speedItemDuration -= Time.deltaTime;
        }
        else
        {
            speedItemDuration = 0;
        }
    }

    public void setMovement()
    {
        Debug.Log("in set movement");
        movementSpeed = 1f;
    }

    private void MovementFromInput()
    {

        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;

        animator.SetFloat("Forward", forwardSpeed);
        characterController.SimpleMove(speed);
    }

    private void reactOnUseSpeedItem()
    {
        speedItemDuration = 10f;
        speedItemIsUsed = true;

    }

    private void reactOnTeleportField()
    {
        onTeleport = true;
    }

    private void reactOnTeleportLeft()
    {

        onTeleport = false;
    }

    private void reactOnSlowTrap()
    {
        Debug.Log("in react ");
        movementSpeed = 1f;
    }


}
