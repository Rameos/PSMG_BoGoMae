using UnityEngine;
using System.Collections;


public class DroneController : MonoBehaviour {

    private float moveSpeed = 10;
    private float turnSpeed = 5;

 

    void Start()
    {

      
    }

    void Update()
    {
        movePlayerWithInput();
    }


    
    private void movePlayerWithInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

}
