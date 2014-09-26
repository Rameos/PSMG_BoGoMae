using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{

    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
    private GameObject refugee;
    private PlayerController playerController;
    private CharacterController characterController;

    void Start()
    {
        refugee = GameObject.FindGameObjectWithTag("Refugee");
        playerController = refugee.GetComponent<PlayerController>();
        characterController = refugee.GetComponent<CharacterController>();
        thirdPersonCamera.enabled = true;
        firstPersonCamera.enabled = false;
        GameeventManager.onLookAroundClickedHandler += reactOnLookAroundClicked;
    }

    void Update()
    {

    }

    private void reactOnLookAroundClicked(int counter)
    {

        if (counter % 2 == 0)
        {
            firstPersonCamera.enabled = false;
        }
        else
        {
            playerController.firstPersonCamera = firstPersonCamera;
            firstPersonCamera.enabled = true;
        }
    }
}