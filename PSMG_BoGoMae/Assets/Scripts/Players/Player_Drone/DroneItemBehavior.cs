using UnityEngine;
using System.Collections;

public class DroneItemBehavior : MonoBehaviour {

    private float inventoryXposition = 300;
    private float inventoryYposition = 10;
    private float inventoryWidth = 500;
    private float inventoryHeight = 50;

    private float trapButtonXposition = 315;
    private float trapButtonYposition = 15;
    private float trapButtonWidth = 150;
    private float trapButtonHeight = 40;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        
        GUI.Box(new Rect(10, 10, 250, 90), "Player Menu");
        

        if (GUI.Button(new Rect(20, 40, 230, 20), "Flüchtling Ansicht"))
        {
            Application.LoadLevel("Refugee_TestLevel");
        }

        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");

        if (GUI.Button(new Rect(trapButtonXposition, trapButtonYposition, trapButtonWidth, trapButtonHeight), "Waffe 1"))
        {
           // GameeventManager.setTrap();
        }
        if (GUI.Button(new Rect(trapButtonXposition+160, trapButtonYposition, trapButtonWidth, trapButtonHeight), "Waffe 2"))
        {
            // GameeventManager.setTrap();
        }
        if (GUI.Button(new Rect(trapButtonXposition+320, trapButtonYposition, trapButtonWidth, trapButtonHeight), "Waffe 3"))
        {
            // GameeventManager.setTrap();
        }
    }
}
