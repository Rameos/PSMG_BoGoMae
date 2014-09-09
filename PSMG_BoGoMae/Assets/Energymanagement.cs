using UnityEngine;
using System.Collections;

public class Energymanagement : MonoBehaviour {

    private float energy = 10;
    private float maxEnergy = 100;
    private float energyIncreaseFactor = 1;

    private static Texture2D rectTexture;
    private static GUIStyle rectStyle;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (energy < maxEnergy)
        {
            energy+= energyIncreaseFactor * Time.deltaTime;
        }
        
	}

    void OnGUI()
    {
        GUI.color = Color.white;
        
        /* Style für Balken
        if (rectTexture == null)
        {
            rectTexture = new Texture2D(1, 1);
        }

        if (rectStyle == null)
        {
            rectStyle = new GUIStyle();
        }

        rectTexture.SetPixel(5, 5, Color.grey);
        rectTexture.Apply();

        rectStyle.normal.background = rectTexture;
        

        GUI.Box(new Rect(Screen.width / 2 + 200, Screen.height / 2 - 100, 100, 25), "Energy:" + Mathf.Round(energy) +"/"+maxEnergy);
        GUI.Box(new Rect(Screen.width / 2 + 200, Screen.height / 2 - 100, energy, 25), "" , rectStyle);
        */

        GUI.Box(new Rect(Screen.width / 2 -50, 10, 100, 25), "Energy:" + Mathf.Round(energy) + "/" + maxEnergy);
        GUI.Box(new Rect(Screen.width / 2 -50, 10, energy, 25), "");
    }


}
