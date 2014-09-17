using UnityEngine;
using System.Collections;

public class Energymanagement : MonoBehaviour {

    private float energy = 10f;
    private float maxEnergy = 100f;
    private float energyIncreaseFactor = 10f;

    public Texture2D defaultTexture;
    private static GUIStyle rectStyle;
    public Texture2D akku;


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

    public float Energy
    {
        get { return energy; }
        set
        {
            if (value < 0)
                energy = 0f;
            else
            {
                energy = value;
            }
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

        GUI.DrawTexture(new Rect(Screen.width / 2 -50, 10, 131, 60),  akku);

        
        GUI.Box(new Rect(Screen.width / 2 -26, 18, energy, 45), createTexture(new Color32(0,255,0,150)));
        GUI.skin.box.normal.background = null;
       // createTexture(new Color32(100,100,100,150));
        
    }

    private Texture2D createTexture(Color32 color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
       // texture.alphaIsTransparency = true;
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        return texture;
    }

}
