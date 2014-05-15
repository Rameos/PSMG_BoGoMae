using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    CharacterController characterController;
    public float moveSpeed = 2f;
    public float turnSpeed = 100f;

    public float rotationSpeed = 60.0f;
    public float walkingSpeed = 4.0f;

    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float rotate = Input.GetAxis("Horizontal");
        float strafe = Input.GetAxis("Strafe");

        transform.Rotate(Vector3.up, rotate * rotationSpeed * Time.deltaTime);
        Vector3 direction = this.transform.right * strafe + this.transform.forward * forward;
        characterController.SimpleMove(direction * walkingSpeed);
    }

    void FixedUpdate()
    {

        //movePlayerWithInput();

    }

    private void movePlayerWithInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    void SpeedUp(int speedUp)
    {
        this.walkingSpeed *= speedUp;
    }

	
}
