using UnityEngine;
using System.Collections;

public class ChangeItemInput : MonoBehaviour
{

    //textures
    public Texture2D lookAroundIcon;
    public Texture2D speedIcon;
    public Texture2D shootIcon;
    public Texture2D teleportIcon;

    private float iconDistance = 4f;
    private float inventoryXposition = (Screen.width / 2) - 170f;
    private float inventoryYposition = Screen.height - 100f;
    private float inventoryWidth = 272;
    private float inventoryHeight = 68;

    private float iconWidth = 64;
    private float iconHeight = 64;
    private float iconYposition = Screen.height - 98f;

    private float lookAroundIconXposition;
    private float speedIconXposition;
    private float shootIconXposition;
    private float teleportIconXposition;


    private bool speedIsCollected = false;
    private bool rocketLauncherIsCollected = false;
    private bool fernglasIsCollected = false;
    private bool speedIsUsed = false;
    private bool fernglasIsUsed = false;
    private bool shootingIsUsed = false;
    private bool onTeleport = false;


    private int lookAroundCounter = 0;
    private int shootingCounter = 0;
    private int speedItemCounter = 0;



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

    private float teleportButtonXposition;
    private float teleportButtonYposition = Screen.height - 100f;
    private float teleportButtonWidth = 64;
    private float teleportButtonHeight = 64;

    private Rect speedItemCounterGUIposition = new Rect(250f, 50f, 150f, 50f);
    // Use this for initialization
    void Start()
    {
        lookAroundIconXposition = inventoryXposition + iconDistance;
        speedIconXposition = lookAroundIconXposition + iconWidth + iconDistance;
        shootIconXposition = speedIconXposition + iconWidth + iconDistance;
        teleportIconXposition = shootIconXposition + iconWidth + iconDistance;
        Screen.lockCursor = true;
        GameeventManager.pickUpItemHandler += reactOnChangedItem;
        GameeventManager.onTeleporterFieldHandler += reactOnTeleport;
        GameeventManager.onTeleportLeftHandler += reactOnTeleportLeft;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (fernglasIsCollected)
        {
            if (Input.GetButtonUp("LookAroundItem"))
            {
                lookAroundCounter++;
                GameeventManager.onLookAroundClicked(lookAroundCounter);
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

    private void reactOnTeleport()
    {
        onTeleport = true;
    }

    private void reactOnTeleportLeft()
    {
        onTeleport = false;
    }

    void OnGUI()
    {
        DrawInventory();
        if (fernglasIsCollected)
        {
            DrawIcon(lookAroundIconXposition, iconYposition, iconWidth, iconHeight, lookAroundIcon);
        }
        if (speedIsCollected && speedItemCounter > 0)
        {
            DrawIcon(speedIconXposition, iconYposition, iconWidth, iconHeight, speedIcon);
        }
        if (rocketLauncherIsCollected)
        {
            DrawIcon(shootIconXposition, iconYposition, iconWidth, iconHeight, shootIcon);
        }
        if (onTeleport)
        {
            DrawIcon(teleportIconXposition, iconYposition, iconWidth, iconHeight, teleportIcon);
        }
        if (speedItemCounter > 0)
        {
            GUI.Box(speedItemCounterGUIposition, "Speed-Items : " + speedItemCounter.ToString());
        }
    }

    private void DrawIcon(float xPosition, float yPosition, float width, float height, Texture2D icon)
    {


        if (GUI.Button(new Rect(xPosition, yPosition, width, height), icon))
        {

        }


    }

    private void DrawInventory()
    {
        GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");
    }

}