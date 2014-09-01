using UnityEngine;
using System.Collections;

public class RefugeeFPShooting : MonoBehaviour {

    public float cooldown = 1f;
    public GameObject bulletPrefab;
    
    public GUITexture crosshair;

    private float cooldownRemaining = 0;
    private GazeInputFromAOI gazeInput;
    private bool inShooting = false;


    // Use this for initialization
	void Start () {
        crosshair.enabled = false;
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        GameeventManager.onShootClickedHandler += reactOnEnableShoot;
	}

	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetMouseButtonDown(0) && inShooting && cooldownRemaining <= 0){

            Instantiate(bulletPrefab, Camera.main.transform.position, Camera.main.transform.rotation);

        }
	}
    private void reactOnEnableShoot(int counter)
    {
        if (counter % 2 == 0)
        {
            inShooting = false;
            crosshair.enabled = false;
        }
        else
        {
            inShooting = true;
            crosshair.enabled = true;
        }
    }
}
