﻿using UnityEngine;
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

        if (onTeleport)
        {
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;
            MovementFromInput();
            HandleSpeedItem();
        }
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
