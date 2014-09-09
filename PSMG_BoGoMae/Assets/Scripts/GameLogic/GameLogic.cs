using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {


    private int transmitterCounter;

	// Use this for initialization
	void Start () {
        GameeventManager.transmitterIsDestroydHandler += reactOnTransmitterDestroyd;
        GameeventManager.onGoalReachedHandler += reactOnGoalReached;
        transmitterCounter = GameObject.FindGameObjectsWithTag("Transmitter").Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void reactOnGoalReached()
    {
        Debug.Log("Flüchtling erreicht Ziel, Sieg, hurra!");
    }

    private void reactOnTransmitterDestroyd()
    {
        Debug.Log("in react on transmitter destroyd");
        transmitterCounter--;
        if (transmitterCounter == 0)
        {
            Debug.Log("Refugee hat gewonnen | alle transmitter kaputt");
        }
    }
}
