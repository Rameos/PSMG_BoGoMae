using UnityEngine;
using System.Collections;
using iViewX;

public class FirstPersonCameraController : MonoBehaviour {

    private Vector2 gazePos;
    private float xAxisRotation;
    private float yAxisRotation;
    private float mouseY, y;
    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;
    private float mouseX, x;
    private float rotationY = 0f;
    private float firstPersonLookSpeed = 0.5f;
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 60.0f);
    private float firstPersonRotationDegreePerSecond = 120f;


	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {


        gazePos = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        x = gazePos.x;
        y = Screen.height - gazePos.y;
        Debug.Log(y);
        //mouseX = Input.GetAxis("Mouse X");
        //mouseY = Input.GetAxis("Mouse Y");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");



	}

    void LateUpdate()
    {


        xAxisRotation = (x );
        yAxisRotation = (y);

        Vector2 gazeData = getGazeData(); 


        xAxisRotation += (mouseY * firstPersonLookSpeed);
        yAxisRotation += (mouseX * firstPersonLookSpeed);


        transform.localRotation = Quaternion.Euler(xAxisRotation, yAxisRotation, 0);


    }

    private Vector2 getGazeData()
    {
        Vector2 result = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
        result.y = Screen.height - result.y;

        return result;
    }
}
