﻿using UnityEngine;
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

public class GameeventManager : MonoBehaviour {
    
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
