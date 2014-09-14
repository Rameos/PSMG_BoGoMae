using UnityEngine;
using System.Collections;

public class FlameLife : MonoBehaviour {
    private float flameLife = 8f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        flameLife -= Time.deltaTime;
        if (flameLife <= 0)
        {
            Destroy(gameObject);
        }
	}
}
