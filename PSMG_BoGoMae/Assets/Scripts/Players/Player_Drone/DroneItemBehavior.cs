using UnityEngine;
using System.Collections;

public class DroneItemBehavior : MonoBehaviour {

    private float inventoryXposition = 300;
    private float inventoryYposition = 10;
    private float inventoryWidth = 500;
    private float inventoryHeight = 50;

    private float trapButtonXposition = 425;
    private float trapButtonYposition = 15;
    private float trapButtonWidth = 240;
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


        if (GUI.Button(new Rect(20, 40, 230, 20), "Zurück ins Menü"))
        {
            Application.LoadLevel("MainMenu");
        }

        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");

        if (GUI.Button(new Rect(trapButtonXposition, trapButtonYposition, trapButtonWidth, trapButtonHeight), "Nach Flüchtling suchen"))
        {
            GameeventManager.setTrap();
        }
    }
}
