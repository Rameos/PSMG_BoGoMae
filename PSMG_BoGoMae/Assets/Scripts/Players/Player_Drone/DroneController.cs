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

    private float moveSpeed = 10;
    private float turnSpeed = 10;

 

    void Start()
    {
        GameeventManager.setTrapHandler += reactOnSetTrap;
    }

    void Update()
    {
        movePlayerWithInput();
        setTrap();
    }

    private void reactOnSetTrap()
    {
        onTrapClicked = true;
    }

    private void setTrap()
    {
    
        if (Input.GetMouseButtonDown(0) && onTrapClicked)
        {
            
            GameObject trapGameObject = (GameObject)Instantiate(trap.gameObject, trapSpawnPosition.position, Quaternion.identity);
            onTrapClicked = false;
        }
    }

    void OnGUI()
    {
        
        if(onTrapClicked) {
            GUI.DrawTexture(new Rect(getCrosshairXPosition(), getCrosshairYPosition(), crosshairWidth, crosshairHeight), crosshair);
        }
    }

    private float getCrosshairXPosition()
    {
        return (Screen.width / 2) - (crosshairWidth / 2);
    }

    private float getCrosshairYPosition()
    {
        return (Screen.height / 2) - (crosshairHeight / 2);
    }

    private float getMouseXPosition()
    {
        return Input.mousePosition.x;
    }

    private float getMouseYPosition()
    {
        return Screen.height - Input.mousePosition.y;
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
