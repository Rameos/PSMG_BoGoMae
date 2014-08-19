using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {


    private int transmitterCounter;

	// Use this for initialization
	void Start () {
        GameeventManager.transmitterIsCollectedHandler += reactOnTransmitterCollected;
        transmitterCounter = GameObject.FindGameObjectsWithTag("Transmitter").Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void reactOnTransmitterCollected()
    {
        transmitterCounter--;
        if (transmitterCounter == 0)
        {
            Debug.Log("Refugee hat gewonnen | alle transmitter eingesammelt");
        }
    }
}
