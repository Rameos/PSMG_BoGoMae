using UnityEngine;
using System.Collections;

public class DroneItemBehavior : MonoBehaviour
{
    public Texture2D shootIcon;
    public Texture2D upIcon;
    public Texture2D downIcon;
    public Texture2D lupeIcon;
    public Texture2D slowIcon;
    public Texture2D weaponArmed;
    public Texture2D weaponUnarmed;

    private float iconDistance = 4f;
    private float inventoryXposition = (Screen.width/2)-170f;
    private float inventoryYposition = Screen.height - 100f;
    private float inventoryWidth = 340;
    private float inventoryHeight = 68;

    private float iconWidth = 64;
    private float iconHeight = 64;
    private float iconYposition = Screen.height - 98f;

    private float slowIconXposition;
    private float shootIconXposition;
    private float showIconXposition;
    private float upIconXposition;
    private float downIconXposition;

    private int droneShootingCounter = 0;
    private int droneShootingPressedCounter = 0;
    private bool rocketsActivatedOrDeactivated;
    private Rect rocketsStatusGUIposition = new Rect(Screen.width / 2 - 200, 10, 50, 50);
    private RefugeeMovement refugeeMovement;
    private Energymanagement energymanagment;
    private float energyCostsForSlowing = 50f;
    private float energyCostsForShowRefugee = 80;
    private bool notEnoughEnergy = false;
    private bool showBlinkNotification = false;
	private bool showRefugeePressed = false;
    private float notificationTimer = 5f;

    // Use this for initialization
    void Start()
    {
        energymanagment = GetComponent<Energymanagement>();
        downIconXposition = inventoryXposition + iconDistance;
        upIconXposition = downIconXposition + iconWidth + iconDistance;
        showIconXposition = upIconXposition + iconWidth + iconDistance;
        slowIconXposition = showIconXposition + iconWidth + iconDistance;
        shootIconXposition = slowIconXposition + iconWidth + iconDistance;
        Screen.lockCursor = true;

    }

    // Update is called once per frame
    void Update()
    {        
        DetectButtonPress();
        EnableShoot();
        if (showBlinkNotification)
        {
            blinkNotificationTimer();
			LeftEyeClosedTrigger();
		}

        //changeWeapon();
    }

	void LeftEyeClosedTrigger ()
	{
		if (EnergyLeft(energyCostsForShowRefugee) && gazeModel.diamLeftEye == 0)
		{
			Debug.Log("DroneItemBehavior DetectButtonPress");
			notEnoughEnergy = false;
			energymanagment.Energy = energymanagment.Energy - energyCostsForShowRefugee;
			GameeventManager.droneShowEnemy();
			showBlinkNotification = false;
			showRefugeePressed = false;
		}
		else
		{
			notEnoughEnergy = true;
		} 	
	}
	
    private void DetectButtonPress()
    {
        if (Input.GetButtonUp("DroneShoot"))
        {
            droneShootingPressedCounter++;

        }else if (Input.GetButtonUp("DroneSlowTrap"))
        {
            if (EnergyLeft(energyCostsForSlowing))
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
            showBlinkNotification = true;
			showRefugeePressed = true;
        }

    }

    private void blinkNotificationTimer()
    {
        notificationTimer -= Time.deltaTime;
        if (notificationTimer <= 0)
        {
            showBlinkNotification = false;
            Debug.Log(showBlinkNotification);
        }
    }

    private bool EnergyLeft(float itemCosts)
    {
        if (energymanagment.Energy >= itemCosts)
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
            rocketsActivatedOrDeactivated = false;
        }
        else
        {
            GameeventManager.onEnableShoot();
            rocketsActivatedOrDeactivated = true;
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
    private void DrawIcon(float xPosition, float yPosition, float width, float height, Texture2D icon)
    {


        if (GUI.Button(new Rect(xPosition, yPosition, width, height), icon))
        {

        }
        

    }

    void OnGUI()
    {
        if (rocketsActivatedOrDeactivated)
        {
            GUI.DrawTexture(rocketsStatusGUIposition, weaponArmed);
        }
        else
        {
            GUI.DrawTexture(rocketsStatusGUIposition, weaponUnarmed);
        }
        

        DrawIcon(downIconXposition, iconYposition, iconWidth, iconHeight, downIcon);
        DrawIcon(upIconXposition, iconYposition, iconWidth, iconHeight, upIcon);
        DrawIcon(showIconXposition, iconYposition, iconWidth, iconHeight, lupeIcon);
        DrawIcon(slowIconXposition, iconYposition, iconWidth, iconHeight, slowIcon);
        DrawIcon(shootIconXposition, iconYposition, iconWidth, iconHeight, shootIcon);
        if (showBlinkNotification)
        {
            GUI.Label(new Rect((Screen.width / 2)-200f, Screen.height / 2, 400f, 100f), "linkes Auge geschlossen halten, um Flüchtling Position anzuzeigen");
        }
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
