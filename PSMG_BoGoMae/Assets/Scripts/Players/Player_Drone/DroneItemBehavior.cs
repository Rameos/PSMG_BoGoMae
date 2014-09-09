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
        }

    }

    private void EnableShoot()
    {
        if (droneShootingPressedCounter % 2 == 0)
        {
            GameeventManager.onDisableShoot();
        }
        else
        {
            GameeventManager.onEnableShoot();
        }

    }



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

    private void DrawShootingIcon()
    {

        if (true)
        {
            if (GUI.Button(new Rect(shootButtonXposition, shootButtonYposition, shootButtonWidth, shootButtonHeight), shootIcon))
            {
                //fuer mausklicks:
                droneShootingCounter++;
                Debug.Log("shooting counter: " + droneShootingCounter);
                if (droneShootingCounter % 2 == 0)
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
