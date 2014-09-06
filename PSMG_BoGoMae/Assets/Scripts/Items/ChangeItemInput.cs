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

    private float inventoryXposition = 300;
    private float inventoryYposition = Screen.height - 100f;
    private float inventoryWidth = 300;
    private float inventoryHeight = 72;

    private float scopeButtonXposition = 310;
    private float scopeButtonYposition = Screen.height - 100f;
    private float scopeButtonWidth = 64;
    private float scopeButtonHeight = 64;

    private float speedButtonXposition = 380;
    private float speedButtonYposition = Screen.height - 100f;
    private float speedButtonWidth = 64;
    private float speedButtonHeight = 64;

    private float shootButtonXposition = 450;
    private float shootButtonYposition = Screen.height - 100f;
    private float shootButtonWidth = 64;
    private float shootButtonHeight = 64;

	// Use this for initialization
	void Start () {
        Screen.lockCursor = true;
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
    }

    private void DrawInventory()
    {
        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");
    }

    private void DrawShootingIcon()
    {

        if(true)
        {
            if (GUI.Button(new Rect(shootButtonXposition, shootButtonYposition, shootButtonWidth, shootButtonHeight), shootIcon) || Input.GetButtonUp("ShootItem"))
            {
                shootingCounter++;
                Debug.Log("shotting counter: "+shootingCounter);
                if (shootingCounter % 2 == 0)
                {
                    //GameeventManager.onDisableShoot();
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
        if (speedIsCollected && !speedIsUsed)
        {
            if (GUI.Button(new Rect(speedButtonXposition, speedButtonYposition, speedButtonWidth, speedButtonHeight), speedIcon) || Input.GetButton("SpeedItem"))
            {
                GameeventManager.useSpeed();
                speedIsUsed = true;
            }

        }
    }
}
