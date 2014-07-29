using UnityEngine;
using System.Collections;
using iViewX;

public class FirstPersonCameraController : MonoBehaviour {

    private Vector2 gazePos;
    private float xAxisRotation;
    private float yAxisRotation;
    private float mouseY, y;
  
    private float mouseX, x;
    private float rotationY = 0f;
    private float firstPersonLookSpeed = 0.5f;
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 60.0f);
    private float firstPersonRotationDegreePerSecond = 120f;
    GazeInputFromAOI gazeInput;
    [SerializeField]
    private float rotationSpeed = 2f;
    private bool lookAroundWithMouse = true;

	// Use this for initialization
	void Start () {
        GameeventManager.enableFirstPersonCameraHandler += reactOnEnableFirstPersonCamera;
        GameeventManager.disableFirstPersonCameraHandler += reactOnDisableFirstPersonCamera;
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        
	}
	
	// Update is called once per frame
	void Update () {

        
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

	}

    void LateUpdate()
    {

        if (lookAroundWithMouse)
        {
            rotateCamWithMouse();

        }
        else
        {
            rotateCamWithGaze();
        }


    }

    private void rotateCamWithMouse()
    {
        xAxisRotation += (mouseY * firstPersonLookSpeed) * -1f;
        yAxisRotation += (mouseX * firstPersonLookSpeed);

        transform.localRotation = Quaternion.Euler(xAxisRotation, yAxisRotation, 0);
    }

    private void rotateCamWithGaze()
    {
        float inputHorizontal = Input.GetAxis("Horizontal") + gazeInput.checkGazeInput();
        float inputVertical = Input.GetAxis("Vertical") + gazeInput.checkGazeInput();
        Debug.Log("christoph" + gazeInput.checkGazeInput());

        gameObject.transform.Rotate(inputVertical * rotationSpeed * -1, 0, 0);
    }


    private void reactOnEnableFirstPersonCamera()
    {
        lookAroundWithMouse = false;
        disableMainCamera();
    }

    private void reactOnDisableFirstPersonCamera()
    {
        lookAroundWithMouse = true;
        enableMainCamera();
    }

    private static void disableMainCamera()
    {
        //Camera.main.enabled = false;
    }

    private static void enableMainCamera()
    {
        Camera.main.enabled = true;
    }

}
