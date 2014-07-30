using UnityEngine;
using System.Collections;
using iViewX;

public class FirstPersonCameraController : MonoBehaviour {

    private Vector2 gazePos;
    private float xAxisRotation;
    private float yAxisRotation;
    private float xAxisWithLimit;
    private float yAxisWithLimit;
    private float xAxisMin = -30;
    private float xAxisMax = 30;
    private float yAxisMin = -360;
    private float yAxisMax = 360;
    private bool inFirstPerson = false;

    public Texture2D crosshair;
    private float crosshairWidth = 32;
    private float crosshairHeight = 32;

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


	}

    void LateUpdate()
    {

        if (lookAroundWithMouse)
        {


        }
        else
        {
            rotateCamWithGaze();
        }


    }

    void OnGUI()
    {

        if (inFirstPerson)
        {
            GUI.DrawTexture(new Rect(getCrosshairXPosition(), getCrosshairYPosition(), crosshairWidth, crosshairHeight), crosshair);
        }
    }


    private float getCrosshairXPosition()
    {

        return (gazeModel.posGazeLeft.x + gazeModel.posGazeRight.x) * 0.5f; 
    }

    private float getCrosshairYPosition()
    {
        return (gazeModel.posGazeLeft.y + gazeModel.posGazeRight.y) * 0.5f;
    }



    private void rotateCamWithGaze()
    {
        float inputXAxis = Input.GetAxis("Vertical") + gazeInput.gazeRotationSpeedXAxis();
        xAxisWithLimit += inputXAxis;
        float inputYAxis = Input.GetAxis("Horizontal") + gazeInput.gazeRotationSpeedYAxis();
        yAxisWithLimit += inputYAxis;

        xAxisWithLimit = Mathf.Clamp(xAxisWithLimit, xAxisMin, xAxisMax);  
        yAxisWithLimit = Mathf.Clamp(yAxisWithLimit, yAxisMin, yAxisMax);

        transform.rotation = Quaternion.Euler(xAxisWithLimit, yAxisWithLimit, 0);
    }

    private float rotateAxisWithLimit(float input)
    {
         return input;
        
    }


    private void reactOnEnableFirstPersonCamera()
    {
        inFirstPerson = true;
        lookAroundWithMouse = false;
        disableMainCamera();
    }

    private void reactOnDisableFirstPersonCamera()
    {
        inFirstPerson = false;
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
