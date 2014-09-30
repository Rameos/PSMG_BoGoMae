using UnityEngine;
using System.Collections;

public class CharacterControllerLogic : MonoBehaviour {

    public Animator animator;
    public float DirectionDampTime = 0.25f;

    private float speed = 0.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        if (animator.layerCount >= 2)
        {
            animator.SetLayerWeight(1, 1);
        }
	}
	
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        speed = new Vector2(horizontal, vertical).sqrMagnitude;

        animator.SetFloat("Speed", speed);
	
	}
}
