using UnityEngine;
using System.Collections;


public delegate void pickUpItem(int itemType);
public delegate void useSpeed();
public delegate void setTrap();
public delegate void refugeeIsActive();
public delegate void droneIsActive();
public delegate void transmitterCollected();
public delegate void onLookAroundClicked(int counter);
public delegate void onShootClicked(int counter);



public class GameeventManager : MonoBehaviour {
    
    public static event pickUpItem pickUpItemHandler;
    public static event useSpeed useSpeedHandler;
    public static event setTrap setTrapHandler;
    public static event refugeeIsActive refugeeIsActiveHandler;
    public static event droneIsActive droneIsActiveHandler;
    public static event transmitterCollected transmitterIsCollectedHandler;
    public static event onLookAroundClicked onLookAroundClickedHandler;
    public static event onShootClicked onShootClickedHandler;

    public static void onShootClicked(int counter)
    {
        if (onShootClickedHandler != null)
        {
            onShootClickedHandler(counter);

        }
    }

    public static void onLookAroundClicked(int counter)
    {
        if (onLookAroundClickedHandler != null)
        {
            onLookAroundClickedHandler(counter);
            
        }
    }

    public static void transmitterCollected()
    {
        if (transmitterIsCollectedHandler != null)
        {
            transmitterIsCollectedHandler();
        }
    }

    public static void refugeeIsActive()
    {
        if (refugeeIsActiveHandler != null)
        {
            refugeeIsActiveHandler();
        }
    }

    public static void droneIsActive()
    {
        if (droneIsActiveHandler != null)
        {
            droneIsActiveHandler();
        }
    }

    public static void pickUpItem(int itemType)
    {
        if (pickUpItemHandler != null)
        {
            pickUpItemHandler(itemType);
        }
    }

    public static void useSpeed()
    {
        if (useSpeedHandler != null)
        {
            useSpeedHandler();
        }
    }

    public static void setTrap()
    {
        if (setTrapHandler != null)
        {
            setTrapHandler();
        }
    }

}
