﻿using UnityEngine;
using System.Collections;

public class FirstPersonCameraController : MonoBehaviour {

    private float xAxisRotation;
    private float yAxisRotation;
    private float mouseY;
    private float mouseX;
    private float firstPersonLookSpeed = 0.5f;
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 60.0f);
    private float firstPersonRotationDegreePerSecond = 120f;


	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


	}

    void LateUpdate()
    {
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
