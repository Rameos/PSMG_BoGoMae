using UnityEngine;
using System.Collections;

public class ChangeItemInput : MonoBehaviour {

    private bool speedIsCollected = false;
    private bool speedIsUsed = false;
    private bool lookAroundIsUsed = false;
    private int lookAroundCounter = 0;

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
        if (id == Config.SPEED)
        {
            speedIsCollected = true;
        }
        if (id == Config.TRANSMITTER)
        {

        }

    }

    void OnGUI()
    {
        UseLookAround();

        UseSpeedItem();
    }

    private void UseLookAround()
    {
        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");

        if (GUI.Button(new Rect(scopeButtonXposition, scopeButtonYposition, scopeButtonWidth, scopeButtonHeight), "umsehen on / off"))
        {
            lookAroundCounter++;
            GameeventManager.onLookAroundClicked(lookAroundCounter);
        }
    }

    private void UseSpeedItem()
    {
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
