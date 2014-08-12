using UnityEngine;
using System.Collections;

public class ChangeItemInput : MonoBehaviour {

    private bool scopeIsCollected = false;
    private bool speedIsCollected = false;
    private bool speedIsUsed = false;
    private bool scopeIsUsed = false;

    private float inventoryXposition = 300;
    private float inventoryYposition = 10;
    private float inventoryWidth = 500;
    private float inventoryHeight = 50;

    private float scopeButtonXposition = 310;
    private float scopeButtonYposition = 15;
    private float scopeButtonWidth = 240;
    private float scopeButtonHeight = 40;

    private float speedButtonXposition = 550;
    private float speedButtonYposition = 15;
    private float speedButtonWidth = 240;
    private float speedButtonHeight = 40;

	// Use this for initialization
	void Start () {
        GameeventManager.pickUpItemHandler += reactOnChangedItem;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void reactOnChangedItem(int id)
    {
        if (id == 0)
        {
            scopeIsCollected = true;
        }
        if (id == 1)
        {
            speedIsCollected = true;
        }

    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 250, 90), "Player Menu");


        if (GUI.Button(new Rect(20, 40, 230, 20), "Drohne Ansicht"))
        {
            Application.LoadLevel("Drone_TestLevel");
        }



        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");

        if (scopeIsCollected)
        {
            if (GUI.Button(new Rect(scopeButtonXposition, scopeButtonYposition, scopeButtonWidth, scopeButtonHeight), "Fernglas"))
            {
                if (scopeIsUsed)
                {
                    GameeventManager.stopUsingScope();
                }
                else
                {
                    GameeventManager.useScope();
                    scopeIsUsed = true;
                }
            }

        }

        if (speedIsCollected && !speedIsUsed)
        {
            if (GUI.Button(new Rect(speedButtonXposition, speedButtonYposition, speedButtonWidth, speedButtonHeight), "Speed"))
            {
                GameeventManager.useSpeed();
                speedIsUsed = true;
            }

        }


    }
}
