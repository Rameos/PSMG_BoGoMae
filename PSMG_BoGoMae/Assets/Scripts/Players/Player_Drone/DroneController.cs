using UnityEngine;
using System.Collections;


public class DroneController : MonoBehaviour {


    public Texture2D crosshair;
    public Trap trap;
    public Transform trapSpawnPosition;

    private bool onTrapClicked = false;
    private bool trapUsed = false;

    private float crosshairWidth = 32f;
    private float crosshairHeight = 32f;

    private float moveForwardSpeed = 75f;
    private float moveBackwardSpeed = 75f;
    private float turnSpeed = 20f;
    private float speed = 0f;
    private float decreaseSpeed = 0.05f;
    private float increaseSpeed = 0.15f;
	private float upAndDownSpeed = 150f;
    public Texture2D sound_on;
    public Texture2D sound_off;

    public static bool soundEnabled = true;


    void Start()
    {
        GameeventManager.setTrapHandler += reactOnSetTrap;
    }

    void Update()
    {
         movePlayerWithInput();
         setTrap();

         if (Input.GetButtonDown("Sound"))
         {
             reactOnEnableSound();
         }   

        if(transform.position.y <=1)
        {
            transform.rigidbody.position = new Vector3(transform.position.x,1,transform.position.z);
        }
    }

    private void reactOnSetTrap()
    {
        onTrapClicked = true;
    }

    private void changeWeapon()
    {

    }

    private void setTrap()
    {
    
        if (Input.GetMouseButtonDown(0) && onTrapClicked)
        {
      
        }
    }

    void OnGUI()
    {
        if (soundEnabled)
        {
            GUI.DrawTexture(new Rect(50, 10, 50, 50), sound_on);
        }
        else
        {
            GUI.DrawTexture(new Rect(50, 10, 50, 50), sound_off);
        }
    }

    private void reactOnEnableSound()
    {
        soundEnabled = !soundEnabled;
        playSoundIfEnabled();

    }

    public void playSoundIfEnabled()
    {
        AudioSource sound = GameObject.FindGameObjectWithTag("DroneCamera").GetComponent<AudioSource>();
        if (soundEnabled)
        {
            sound.audio.Play();
        }
        else
        {
            sound.Stop();
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


    private float getMouseXPosition()
    {
        return Input.mousePosition.x;
    }

    private float getMouseYPosition()
    {
        return Screen.height - Input.mousePosition.y;
    }

    private void handleRotation()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    
    private void movePlayerWithInput()
    {

        handleRotation();

        if (Input.GetKey(KeyCode.W))
        {
            if (speed < moveForwardSpeed)
            {
                speed += increaseSpeed;
            }
            if (speed > 0)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime, transform);
            }
            handleRotation();
        }


        if (Input.GetKey(KeyCode.S))
        {

            if (speed > -moveForwardSpeed)
            {
                speed += -increaseSpeed;
            }
            if (speed < 0)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime, transform);
            }
            handleRotation();
        }


        if (Input.GetKey(KeyCode.W) == false || Input.GetKey(KeyCode.S) == false)
        {
            if (speed < 0)
            {
                speed += decreaseSpeed;
                transform.Translate(Vector3.forward * speed * Time.deltaTime, transform);
            }

            if (speed > 0)
            {
                speed -= decreaseSpeed;
                transform.Translate(Vector3.forward * speed * Time.deltaTime, transform);
            }
            handleRotation();
        }

		if (Input.GetKey (KeyCode.Q)) {
			if(transform.position.y > 10){
			transform.Translate(Vector3.down * upAndDownSpeed * Time.deltaTime, transform);
			}
		}

		if (Input.GetKey (KeyCode.E)) {
			if(transform.position.y < 3000){
				transform.Translate(Vector3.up * upAndDownSpeed * Time.deltaTime, transform);
			}
		}

        
    }




}
