using UnityEngine;
using System.Collections;

public class ChangeItemInput : MonoBehaviour {

    private bool speedIsCollected = false;
    private bool speedIsUsed = false;
    private bool lookAroundIsUsed = false;
    private bool shootingIsUsed = false;
    private int lookAroundCounter = 0;
    private int shootingCounter = 0;

    private float inventoryXposition = 300;
    private float inventoryYposition = 10;
    private float inventoryWidth = 1000;
    private float inventoryHeight = 50;

    private float scopeButtonXposition = 310;
    private float scopeButtonYposition = 15;
    private float scopeButtonWidth = 240;
    private float scopeButtonHeight = 40;

    private float speedButtonXposition = 550;
    private float speedButtonYposition = 15;
    private float speedButtonWidth = 240;
    private float speedButtonHeight = 40;

    private float shootButtonXposition = 790;
    private float shootButtonYposition = 15;
    private float shootButtonWidth = 240;
    private float shootButtonHeight = 40;

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
        UseShooting();
        UseSpeedItem();
    }

    private void UseShooting()
    {
        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");

        if (GUI.Button(new Rect(shootButtonXposition, shootButtonYposition, shootButtonWidth, shootButtonHeight), "schießen"))
        {
            shootingCounter++;
            GameeventManager.onShootClicked(shootingCounter);
        }
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
