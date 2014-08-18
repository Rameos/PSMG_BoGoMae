using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {


    private GameObject[] transmitterArray;

	// Use this for initialization
	void Start () {
        GameeventManager.transmitterIsCollectedHandler += reactOnTransmitterCollected;
        transmitterArray = GameObject.FindGameObjectsWithTag("Transmitter");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void reactOnTransmitterCollected()
    {
        transmitterArray = GameObject.FindGameObjectsWithTag("Transmitter");
        Debug.Log("transmitter array laenge:" +transmitterArray.Length);
        if (transmitterArray.Length == 0)
        {
            Debug.Log("Refugee hat gewonnen | alle transmitter eingesammelt");
        }
    }
}
