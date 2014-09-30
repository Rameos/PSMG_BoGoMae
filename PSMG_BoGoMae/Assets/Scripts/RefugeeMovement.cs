using UnityEngine;
using System.Collections;

public class RefugeeMovement : MonoBehaviour
{

    Animator animator;
    private CharacterController characterController;
    private float movementSpeed = 50f;
    private float movementSpeedDefault = 50f;
    private float movementSpeedWithItem = 150f;
    private float movementSpeedSlowed;
    private float speedItemDuration = 10f;
    private float slowItemDuration = 0f;
    private bool speedItemIsUsed = false;
    private bool slowItemIsUsed = false;
    private Rect speedItemDurationGUIposition = new Rect(400f, 50f, 150f, 50f);
    private Rect slowItemDurationGUIposition = new Rect(550f, 50f, 150f, 50f);
    private bool onTeleport = false;
    private Vector3 curVel = Vector3.zero;
    private float friction = 0.4f;

    public bool showDiscoveredMessage;
    private bool showSlowedMessage;

    private float discoveredMessageDuration = 4f;
    private float slowedMessageduration = 4f;

    public Texture2D slowActivatedTexture;
    public Texture2D discoveredActivatedTexture;

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
        if (speedItemIsUsed)
        {
            HandleSpeedItem();

        }
        if (slowItemIsUsed)
        {
            HandleSlowItem();
        }
        if (onTeleport)
        {

        }
        else
        {
            MovementFromInput();
            if (speedItemIsUsed)
            {
                HandleSpeedItem();

            }
        }
    }



    void OnGUI()
    {
        if (speedItemIsUsed && speedItemDuration > 0)
        {
            GUI.Box(speedItemDurationGUIposition, "Speed-Boost Zeit: " + speedItemDuration.ToString("0"));
        }
        if (slowItemIsUsed && slowItemDuration > 0)
        {
            GUI.Box(slowItemDurationGUIposition, "Slow Effect: " + slowItemDuration.ToString("0"));
        }
        if (showDiscoveredMessage)
        {
            ShowDiscoveredNotification();
        }
        if (showSlowedMessage)
        {
            ShowSlowedNotification();
        }
    }

    private void ShowDiscoveredNotification()
    {
        discoveredMessageDuration -= Time.deltaTime;
        if (discoveredMessageDuration >= 0)
        {
            GUI.Box(new Rect((Screen.width / 2) - 200f, Screen.height / 2, 400f, 100f), discoveredActivatedTexture);
        }
        else
        {
            showDiscoveredMessage = false;
            discoveredMessageDuration = 4f;
        }

    }

    private void ShowSlowedNotification()
    {
        slowedMessageduration -= Time.deltaTime;
        if (slowedMessageduration >= 0)
        {
            GUI.Box(new Rect((Screen.width / 2) - 200f, Screen.height / 2, 400f, 100f), slowActivatedTexture);
        }
        else
        {
            showSlowedMessage = false;
            slowedMessageduration = 4f;
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

    private void HandleSlowItem()
    {
        CheckSlowItemDuration();
        if (slowItemIsUsed && slowItemDuration > 0)
        {
            showSlowedMessage = true;
            movementSpeed = movementSpeedSlowed;
        }
        else
        {
            movementSpeed = movementSpeedDefault;
            slowItemIsUsed = false;
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

    public void setMovementSpeedTo(float speed)
    {
        slowItemIsUsed = true;
        slowItemDuration += 5f;
        movementSpeedSlowed = speed;
    }

    private void CheckSlowItemDuration()
    {
        if (slowItemDuration >= 0)
        {
            slowItemDuration -= Time.deltaTime;
        }
        else
        {
            slowItemDuration = 0;
        }
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


}
