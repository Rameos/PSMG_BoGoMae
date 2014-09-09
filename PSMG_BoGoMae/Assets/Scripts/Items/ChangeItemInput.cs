using UnityEngine;
using System.Collections;

public class ChangeItemInput : MonoBehaviour {

    //textures
    public Texture2D lookAroundIcon;
    public Texture2D speedIcon;
    public Texture2D shootIcon;

    private bool speedIsCollected = false;
    private bool rocketLauncherIsCollected = false;
    private bool fernglasIsCollected = false;
    private bool speedIsUsed = false;
    private bool lookAroundIsUsed = false;
    private bool shootingIsUsed = false;
    private int lookAroundCounter = 0;
    private int shootingCounter = 0;
    private int speedItemCounter = 0;

    private float inventoryXposition = (Screen.width / 2) - 150f;
    private float inventoryYposition = Screen.height - 100f;
    private float inventoryWidth = 300;
    private float inventoryHeight = 72;

    private float scopeButtonXposition;
    private float scopeButtonYposition = Screen.height - 100f;
    private float scopeButtonWidth = 64;
    private float scopeButtonHeight = 64;

    private float speedButtonXposition;
    private float speedButtonYposition = Screen.height - 100f;
    private float speedButtonWidth = 64;
    private float speedButtonHeight = 64;

    private float shootButtonXposition;
    private float shootButtonYposition = Screen.height - 100f;
    private float shootButtonWidth = 64;
    private float shootButtonHeight = 64;

    private Rect speedItemCounterGUIposition = new Rect(250f, 50f, 150f, 50f);
	// Use this for initialization
	void Start () {
        scopeButtonXposition = inventoryXposition;
        speedButtonXposition = inventoryXposition + speedButtonWidth;
        shootButtonXposition = inventoryXposition + (shootButtonWidth) * 2f;
        Screen.lockCursor = true;
        GameeventManager.pickUpItemHandler += reactOnChangedItem;
	}
	
	// Update is called once per frame
	void Update () {
        DetectButtonPress();
        EnableShoot();
	}

    private void DetectButtonPress()
    {
        if (rocketLauncherIsCollected)
        {
            if (Input.GetButtonUp("ShootItem"))
            {
                shootingCounter++;
            }
        }
        if (speedIsCollected && speedItemCounter > 0)
        {
            if (Input.GetButtonUp("SpeedItem"))
            {
                GameeventManager.useSpeed();
                speedItemCounter--;
                speedIsUsed = true;
            }
        }


    }

    private void EnableShoot()
    {
        if (shootingCounter % 2 == 0)
        {
            GameeventManager.onDisableShoot();
        }
        else
        {
            GameeventManager.onEnableShoot();
        }

    }

    private void reactOnChangedItem(int id)
    {
        if (id == Config.SPEED)
        {
            speedIsCollected = true;

            speedItemCounter++;
        }
        if (id == Config.TRANSMITTER)
        {

        }
        if (id == Config.ROCKETLAUNCHER)
        {
            rocketLauncherIsCollected = true;
        }
        if (id == Config.FERNGLAS)
        {
            fernglasIsCollected = true;
        }

    }

    void OnGUI()
    {
        DrawInventory();
        DrawFernglasIcon();
        DrawShootingIcon();
        DrawSpeedIcon();
        if (speedItemCounter > 0)
        {
            GUI.Box(speedItemCounterGUIposition, "Speed-Items : " + speedItemCounter.ToString());
        }
    }



    private void DrawInventory()
    {
        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");
    }

    private void DrawShootingIcon()
    {

        if(rocketLauncherIsCollected)
        {
            if (GUI.Button(new Rect(shootButtonXposition, shootButtonYposition, shootButtonWidth, shootButtonHeight), shootIcon))
            {
                shootingCounter++;
                Debug.Log("shotting counter: "+shootingCounter);
                if (shootingCounter % 2 == 0)
                {
                    GameeventManager.onDisableShoot();
                }
                else
                {
                    GameeventManager.onEnableShoot();
                }
            }

        }
    }

    private void DrawFernglasIcon()
    {
        //GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");
        if (fernglasIsCollected)
        {
            if (GUI.Button(new Rect(scopeButtonXposition, scopeButtonYposition, scopeButtonWidth, scopeButtonHeight), lookAroundIcon) || Input.GetButton("LookAroundItem"))
            {
                lookAroundCounter++;
                GameeventManager.onLookAroundClicked(lookAroundCounter);
            }

        }
    }

    private void DrawSpeedIcon()
    {
        if (speedIsCollected && speedItemCounter > 0)
        {
            if (GUI.Button(new Rect(speedButtonXposition, speedButtonYposition, speedButtonWidth, speedButtonHeight), speedIcon))
            {
                GameeventManager.useSpeed();
                speedIsUsed = true;
            }

        }
    }
}
