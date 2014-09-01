using UnityEngine;
using System.Collections;

public class RefugeeFPShooting : MonoBehaviour {

    public GameObject bulletPrefab;
    private GazeInputFromAOI gazeInput;
    private float bulletImpulse = 20f;
    private bool inFirstPerson = false;

    // Use this for initialization
	void Start () {
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        GameeventManager.onLookAroundClickedHandler += reactOnEnableFirstPersonCamera;
	}

	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetButtonDown("Fire1") && inFirstPerson){
           Camera camera = Camera.main;
           GameObject bullet = (GameObject) Instantiate(bulletPrefab, transform.position, transform.rotation);
           bullet.rigidbody.AddForce(camera.transform.forward * bulletImpulse, ForceMode.Impulse);
        }
	}
    private void reactOnEnableFirstPersonCamera(int counter)
    {
        if (counter % 2 == 0)
        {
            inFirstPerson = false;
        }
        else
        {
            inFirstPerson = true;
        }
    }
}
