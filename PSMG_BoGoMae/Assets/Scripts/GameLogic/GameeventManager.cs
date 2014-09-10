using UnityEngine;
using System.Collections;


public delegate void pickUpItem(int itemType);
public delegate void useSpeed();
public delegate void setTrap();
public delegate void refugeeIsActive();
public delegate void droneIsActive();
public delegate void transmitterDestroyd();
public delegate void onLookAroundClicked(int counter);
public delegate void onEnableShoot();
public delegate void onDisableShoot();
public delegate void onGoalReached();
public delegate void onTeleporterField();
public delegate void onTeleportLeft();
public delegate void onTeleportPressed();
public delegate void droneFiredARocket();
public delegate void droneSetSlowTrap();
public delegate void enableSound();



public class GameeventManager : MonoBehaviour
{

    public static event pickUpItem pickUpItemHandler;
    public static event useSpeed useSpeedHandler;
    public static event setTrap setTrapHandler;
    public static event refugeeIsActive refugeeIsActiveHandler;
    public static event droneIsActive droneIsActiveHandler;
    public static event transmitterDestroyd transmitterIsDestroydHandler;
    public static event onLookAroundClicked onLookAroundClickedHandler;
    public static event onEnableShoot onEnableShootHandler;
    public static event onDisableShoot onDisableShootHandler;
    public static event onGoalReached onGoalReachedHandler;
    public static event onTeleporterField onTeleporterFieldHandler;
    public static event onTeleportLeft onTeleportLeftHandler;
    public static event onTeleportPressed onTeleportPressedHandler;
    public static event droneFiredARocket onDroneFiredARocketHandler;
    public static event droneSetSlowTrap onDroneSetSlowTrapHandler;
    public static event enableSound onEnableSoundHandler;

    public static void droneSetSlowTrap()
    {
        if (onDroneSetSlowTrapHandler != null)
        {
            onDroneSetSlowTrapHandler();
        }
    }


    public static void enableSound()
    {
        if (onEnableSoundHandler != null)
        {
            onEnableSoundHandler();

        }
    }

    public static void droneFiredARocket()
    {
        if (onDroneFiredARocketHandler != null)
        {
            onDroneFiredARocketHandler();
        }
}

    public static void onTeleportPressed()
    {
        if (onTeleportPressedHandler != null)
        {
            onTeleportPressedHandler();
        }
    }

    public static void onTeleportLeft()
    {
        if (onTeleportLeftHandler != null)
        {
            onTeleportLeftHandler();
        }
    }

    public static void onTeleporterField()
    {
        if (onTeleporterFieldHandler != null)
        {
            onTeleporterFieldHandler();
        }
    }

    public static void onGoalReached()
    {
        if (onGoalReachedHandler != null)
        {
            onGoalReachedHandler();
        }
    }

    public static void onDisableShoot()
    {
        if (onDisableShootHandler != null)
        {
            onDisableShootHandler();
        }
    }

    public static void onEnableShoot()
    {
        if (onEnableShootHandler != null)
        {
            onEnableShootHandler();

        }
    }

    public static void onLookAroundClicked(int counter)
    {
        if (onLookAroundClickedHandler != null)
        {
            onLookAroundClickedHandler(counter);

        }
    }

    public static void transmitterDestroyd()
    {
        if (transmitterIsDestroydHandler != null)
        {
            transmitterIsDestroydHandler();
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
