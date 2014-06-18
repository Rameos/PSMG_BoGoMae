using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{

    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
    public Camera droneCamera;
    private GameObject refugee;
    private GameObject drone;

    void Start()
    {
        refugee = GameObject.Find("Player");
        drone = GameObject.Find("DroneCube");
        //drone.gameObject.SetActive(false);
        thirdPersonCamera.enabled = true;
        firstPersonCamera.enabled = false;
        droneCamera.enabled = false;
    }

    void Update()
    {

        if (Input.GetButton("ExitFirstPersonView"))
        {
            thirdPersonCamera.enabled = true;
            firstPersonCamera.enabled = false;
        }
    }

    void OnGUI()
    {

        GUI.Box(new Rect(10, 10, 100, 90), "Player Menu");


        if (GUI.Button(new Rect(20, 40, 80, 20), "Refugee"))
        {
            //refugee.renderer.enabled = true;

            //drone.renderer.enabled = false;
            thirdPersonCamera.enabled = true;
            firstPersonCamera.enabled = false;
            droneCamera.enabled = false;
        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "Drone"))
        {

            //refugee.renderer.enabled = false;
            //drone.renderer.enabled = true;
            thirdPersonCamera.enabled = false;
            firstPersonCamera.enabled = false;
            droneCamera.enabled = true;
        }
    }
}