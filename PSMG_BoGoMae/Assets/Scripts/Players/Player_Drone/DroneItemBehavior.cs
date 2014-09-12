using UnityEngine;
using System.Collections;

public class DroneItemBehavior : MonoBehaviour
{

    private float inventoryXposition = 300;
    private float inventoryYposition = Screen.height - 100f;
    private float inventoryWidth = 500;
    private float inventoryHeight = 50;

    private float trapButtonXposition = 315;
    private float trapButtonYposition = 15;
    private float trapButtonWidth = 150;
    private float trapButtonHeight = 40;

    private float shootButtonXposition = 450;
    private float shootButtonYposition = Screen.height - 100f;
    private float shootButtonWidth = 64;
    private float shootButtonHeight = 64;

    private int droneShootingCounter = 0;
    public Texture2D shootIcon;
    private int droneShootingPressedCounter = 0;
    private string rocketsActivatedOrDeactivated;
    private Rect rocketsStatusGUIposition = new Rect(Screen.width / 2 - 200, 10, 150, 25);
    private RefugeeMovement refugeeMovement;

    // Use this for initialization
    void Start()
    {

        Screen.lockCursor = true;

    }

    // Update is called once per frame
    void Update()
    {
        DetectButtonPress();
        EnableShoot();


        //changeWeapon();
    }

    private void DetectButtonPress()
    {
        if (Input.GetButtonUp("DroneShoot"))
        {
            droneShootingPressedCounter++;
        }else if (Input.GetButtonUp("DroneSlowTrap"))
        {
            GameeventManager.droneSetSlowTrap();
        }
        else if (Input.GetButtonUp("DroneShowEnemy"))
        {
            GameeventManager.droneShowEnemy();
        }

    }

    private void EnableShoot()
    {
        if (droneShootingPressedCounter % 2 == 0)
        {
            GameeventManager.onDisableShoot();
            rocketsActivatedOrDeactivated = "deaktiviert";
        }
        else
        {
            GameeventManager.onEnableShoot();
            rocketsActivatedOrDeactivated = "aktiviert";
        }

    }

    /*

    void changeWeapon()
    {
        if (Input.GetButtonUp("DroneShoot"))
        {
            droneShootingCounter++;
            Debug.Log("shotting counter: " + droneShootingCounter);
            if (droneShootingCounter % 2 == 0)
            {
                Screen.lockCursor = true;
                GameeventManager.onDisableShoot();
            }
            else
            {
                GameeventManager.onEnableShoot();
                Screen.lockCursor = false;
            }
        }
    }
    */
    private void DrawShootingIcon()
    {


        if (GUI.Button(new Rect(shootButtonXposition, shootButtonYposition, shootButtonWidth, shootButtonHeight), shootIcon))
        {
            // theoretisch für maus klicks
            if (droneShootingCounter % 2 == 0)
            {
                GameeventManager.onDisableShoot();
            }
            else
            {
                GameeventManager.onEnableShoot();
            }
        }
        GUI.Box(rocketsStatusGUIposition, "Raketen: " + rocketsActivatedOrDeactivated);

    }

    void OnGUI()
    {

        DrawShootingIcon();
        /*  GUI.Box(new Rect(inventoryXposition, inventoryYposition, inventoryWidth, inventoryHeight), "");

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
         * 
         * */
    }
}
