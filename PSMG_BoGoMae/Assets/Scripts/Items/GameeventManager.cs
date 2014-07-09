using UnityEngine;
using System.Collections;


public delegate void pickUpItem(int itemType);


public class GameeventManager : MonoBehaviour {

    public static event pickUpItem pickUpItemHandler;


    public static void pickUpItem(int itemType)
    {
        if (pickUpItemHandler != null)
        {
            pickUpItemHandler(itemType);
        }
    }

}
