using UnityEngine;
using System.Collections;

public class RefugeeFPShooting : MonoBehaviour {

    public float cooldown = 1f;
    public GameObject bulletPrefab;

    private float cooldownRemaining = 0;
    private GazeInputFromAOI gazeInput;
    private bool inShooting = false;


    // Use this for initialization
	void Start () {
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;
	}

	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetMouseButtonDown(0) && inShooting && cooldownRemaining <= 0){
            Debug.Log("in fps update");
            Instantiate(bulletPrefab, transform.FindChild("Main Camera").FindChild("rocketLauncher").transform.position, Camera.main.transform.rotation);

        }
	}
    private void reactOnEnableShoot()
    {

        Debug.Log("in shooting true");
        inShooting = true;
        transform.FindChild("Main Camera").FindChild("rocketLauncher").transform.gameObject.SetActive(true);

    }

    private void reactOnDisableShoot()
    {

        Debug.Log("in shooting false");
        inShooting = false;
        transform.FindChild("Main Camera").FindChild("rocketLauncher").transform.gameObject.SetActive(false);
    }
}
