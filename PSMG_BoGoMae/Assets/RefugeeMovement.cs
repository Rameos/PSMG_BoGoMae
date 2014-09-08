using UnityEngine;
using System.Collections;

public class RefugeeMovement : MonoBehaviour {

    Animator animator;
    private CharacterController characterController;
    private float movementSpeed = 50f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;
        
        animator.SetFloat("Forward", forwardSpeed);
        characterController.SimpleMove(speed);
	}
}
