using UnityEngine;
using System.Collections;


public delegate void pickUpItem(int itemType);
public delegate void useScope();
public delegate void stopUsingScope();
public delegate void useSpeed();
public delegate void setTrap();
public delegate void enableFirstPersonCamera();
public delegate void disableFirstPersonCamera();
public delegate void refugeeIsActive();


public class GameeventManager : MonoBehaviour {

    public static event pickUpItem pickUpItemHandler;
    public static event useScope useScopeHandler;
    public static event stopUsingScope stopUsingScopeHandler;
    public static event useSpeed useSpeedHandler;
    public static event setTrap setTrapHandler;
    public static event enableFirstPersonCamera enableFirstPersonCameraHandler;
    public static event disableFirstPersonCamera disableFirstPersonCameraHandler;
    public static event refugeeIsActive refugeeIsActiveHandler;

    public static void refugeeIsActive()
    {
        if (refugeeIsActiveHandler != null)
        {
            refugeeIsActiveHandler();
        }
    }

    public static void pickUpItem(int itemType)
    {
        if (pickUpItemHandler != null)
        {
            pickUpItemHandler(itemType);
        }
    }

    public static void useScope()
    {
        if (useScopeHandler != null)
        {
            useScopeHandler();
            enableFirstPersonCameraHandler();
        }
    }

    public static void stopUsingScope()
    {
        if (stopUsingScopeHandler != null)
        {
            stopUsingScopeHandler();
            disableFirstPersonCameraHandler();
        }
    }

    public static void useSpeed()
    {
        if (useScopeHandler != null)
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
