using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{

    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;

    void Start()
    {
        thirdPersonCamera.enabled = true;
        firstPersonCamera.enabled = false;
    }

    void Update()
    {
        /*
        if (Input.GetButton("EnterFirstPersonView"))
        {
            thirdPersonCamera.enabled = false;
            firstPersonCamera.enabled = true;
        }
         */
        if (Input.GetButton("ExitFirstPersonView"))
        {
            thirdPersonCamera.enabled = true;
            firstPersonCamera.enabled = false;
        }
    }

    void PickedUpScope()
    {
        Debug.Log("in pickedupscope");
        thirdPersonCamera.enabled = false;
        firstPersonCamera.enabled = true;
    }
}