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
    private Energymanagement energymanagment;
    private float energyCostsForSlowing = 50f;
    private bool notEnoughEnergy = false;

    // Use this for initialization
    void Start()
    {
        energymanagment = GetComponent<Energymanagement>();
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
            if (EnergyLeft())
            {
                notEnoughEnergy = false;
                energymanagment.Energy = energymanagment.Energy - energyCostsForSlowing;
                GameeventManager.droneSetSlowTrap();
            }
            else
            {
                notEnoughEnergy = true;
            }
        }
        else if (Input.GetButtonUp("DroneShowEnemy"))
        {
            GameeventManager.droneShowEnemy();
        }

    }

    private bool EnergyLeft()
    {
        if (energymanagment.Energy >= energyCostsForSlowing)
        {
            return true;
        }
        else
        {
            return false;
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
        //DrawNotEnoughEnergy();
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

    private void DrawNotEnoughEnergy()
    {
        if (notEnoughEnergy)
        {
            GUI.Label(new Rect(10, 10, 300, 20), "Not enough Energy!");
        }
    }
}
