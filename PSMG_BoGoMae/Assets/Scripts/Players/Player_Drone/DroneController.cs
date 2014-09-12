using UnityEngine;
using System.Collections;


public class DroneController : MonoBehaviour {


    public Texture2D crosshair;
    public Trap trap;
    public Transform trapSpawnPosition;

    private bool onTrapClicked = false;
    private bool trapUsed = false;

    private float crosshairWidth = 32;
    private float crosshairHeight = 32;

    private float moveForwardSpeed = 50;
    private float moveBackwardSpeed = 50;
    private float turnSpeed = 10;
    private float speed = 0;
    private float decreaseSpeed = 0.05f;
    private float increaseSpeed = 0.15f;

    public static bool soundEnabled = false;


    void Start()
    {
        GameeventManager.setTrapHandler += reactOnSetTrap;
    }
    /*
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        Camera camera = GameObject.FindGameObjectWithTag("DroneCamera").camera;
        if (networkView.isMine)
        {
            camera.enabled = true;
        }
        else
        {
            camera.enabled = false;
        }
    }
*/
    void Update()
    {

         movePlayerWithInput();
         setTrap();

         if (Input.GetButtonDown("Sound"))
         {
             reactOnEnableSound();

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
            /*
            Vector3 test = new Vector3(getCrosshairXPosition(), getCrosshairYPosition(), 2);
            Ray ray = Camera.main.ScreenPointToRay(test);
            if (Physics.Raycast(ray)
            GameObject trapGameObject = (GameObject)Instantiate(trap.gameObject, test, Quaternion.identity);
            onTrapClicked = false;
            */
        
        }
    }

    void OnGUI()
    {
        if (soundEnabled)
        {
            GUI.Box(new Rect(100, 10, 100, 50), "Sound an");
        }
      /*  if(onTrapClicked) {
            GUI.DrawTexture(new Rect(getCrosshairXPosition(), getCrosshairYPosition(), crosshairWidth, crosshairHeight), crosshair);
        }
       */
    }

    private void reactOnEnableSound()
    {
        soundEnabled = !soundEnabled;
        Debug.Log("Sound: " + soundEnabled.ToString());
        playSoundIfEnabled();

    }

    private void playSoundIfEnabled()
    {
        AudioSource sound = GameObject.FindGameObjectWithTag("DroneCamera").GetComponent<AudioSource>();
        if (soundEnabled)
        {
            sound.audio.Play();
            Debug.Log(sound);
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
            //Debug.Log("speed " + speed);
            if (speed > -moveForwardSpeed)
            {
                speed += -increaseSpeed;
            }
            if (speed < 0)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime, transform);
                Debug.Log(transform);
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
			transform.Translate(Vector3.down * 50 * Time.deltaTime, transform);
		}

		if (Input.GetKey (KeyCode.E)) {
			if(transform.position.y < 3000){
				transform.Translate(Vector3.up * 50 * Time.deltaTime, transform);
			}
		}

        
    }




}
