using UnityEngine;
using System.Collections;

public class Energymanagement : MonoBehaviour {

    private float energy = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.color = Color.green;
        GUI.backgroundColor = Color.magenta;

        GUI.Box(new Rect(Screen.width / 2 + 200, Screen.height / 2 - 100, 100, 25), "Energy:" + energy +"/100");
    }


}
