using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    private RefugeeMovement refugeeMovement;
    private int transmitterCounter;

	// Use this for initialization
	void Start () {
        GameeventManager.transmitterIsDestroydHandler += reactOnTransmitterDestroyd;
        GameeventManager.onGoalReachedHandler += reactOnGoalReached;
        GameeventManager.onDroneSetSlowTrapHandler += reactOnSlowTrap;
        transmitterCounter = GameObject.FindGameObjectsWithTag("Transmitter").Length;
        Debug.Log(transmitterCounter);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
    private void reactOnSlowTrap()
    {
        GameObject refugee = GameObject.FindGameObjectWithTag("Refugee").gameObject;
        refugeeMovement = refugee.GetComponent<RefugeeMovement>();
        refugeeMovement.setMovement();
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
