// ----------------------------------------------------------------------------
//
// (C) Copyright 2014, Visual Interaction GmbH 
//
// All rights reserved. This work contains unpublished proprietary 
// information of Visual Interaction GmbH and is copy protected by law. 
// (see accompanying file eula.pdf)
//
// ----------------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using iViewX;

public class RotateObjectWhileFocused : MonoBehaviourWithGazeComponent{

    // Setup the RotationSpeed of the Rotation
    public float rotationsPerMinute = 100.0f;

    
    public override void OnGazeEnter(RaycastHit hit)
    {
        Debug.Log("testtesttest");
    }

    //Rotate the Element if the Gaze stays on the Collider
    public override void OnGazeStay(RaycastHit hit)
    {

        Debug.Log("on Gaze Stay");
        transform.Rotate(0, 0, rotationsPerMinute * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    //Reset the Element.Transform when the gaze leaves the Collider
    public override void OnGazeExit()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
