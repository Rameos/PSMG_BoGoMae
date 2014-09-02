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
        
        if(onTrapClicked) {
            GUI.DrawTexture(new Rect(getCrosshairXPosition(), getCrosshairYPosition(), crosshairWidth, crosshairHeight), crosshair);
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
    
    private void movePlayerWithInput()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

}
