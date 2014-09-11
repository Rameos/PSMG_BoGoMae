using UnityEngine;
using System.Collections;

public class RefugeeHealth : MonoBehaviour {

    private float health = 10;
    private float maxHealth = 100;
    private float healthIncreaseFactor = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health < maxHealth)
        {
            health += healthIncreaseFactor * Time.deltaTime;
        }
	
	}

    public float Health
    {
        get { return health; }
        set
        {
            if (value < 0)
                health = 0f;
            else
            {
                health = value;
            }
        }
    }


    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - 50, 10, 100, 25), "Health:" + Mathf.Round(health) + "/" + maxHealth);
        GUI.Box(new Rect(Screen.width / 2 - 50, 10, health, 25), "");
    }
}
