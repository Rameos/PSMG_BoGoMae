using UnityEngine;
using System.Collections;

public class RotateCamWithGaze : MonoBehaviour
{

    GazeInputFromAOI gazeInput;
    [SerializeField]
    private float rotationSpeed = 2f;
    //private bool firstPersonCameraIsActive = false;

    void Start()
    {

        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();

    }

    void Update()
    {
         checkInput();
    }


    private void checkInput()
    {
        float inputHorizontal = Input.GetAxis("Horizontal") + gazeInput.checkGazeInput();
        Debug.Log("christoph" +gazeInput.checkGazeInput());
        gameObject.transform.Rotate(0, inputHorizontal * rotationSpeed, 0);
    }
}
