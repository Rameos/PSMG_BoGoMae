using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{

    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
   // public Camera droneCamera;
    private GameObject refugee;
    //private GameObject drone;

    void Start()
    {
        refugee = GameObject.Find("Player");
        //drone = GameObject.Find("DroneCube");
        //drone.gameObject.SetActive(false);
        thirdPersonCamera.enabled = true;
        firstPersonCamera.enabled = false;
       // droneCamera.enabled = false;
    }

    void Update()
    {

        if (Input.GetButton("ExitFirstPersonView"))
        {
            thirdPersonCamera.enabled = true;
            firstPersonCamera.enabled = false;
        }
    }

}