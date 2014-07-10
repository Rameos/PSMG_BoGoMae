using UnityEngine;
using System.Collections;


public delegate void pickUpItem(int itemType);
public delegate void useScope();
public delegate void useSpeed();


public class GameeventManager : MonoBehaviour {

    public static event pickUpItem pickUpItemHandler;
    public static event useScope useScopeHandler;
    public static event useSpeed useSpeedHandler;


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
        }
    }

    public static void useSpeed()
    {
        if (useScopeHandler != null)
        {
            useSpeedHandler();
        }
    }

}
