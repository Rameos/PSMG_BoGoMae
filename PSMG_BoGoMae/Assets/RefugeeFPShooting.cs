using UnityEngine;
using System.Collections;

public class RefugeeFPShooting : MonoBehaviour {

    public float cooldown = 0.2f;
    public GameObject bulletPrefab;
    public float bulletFireRange = 200.0f;

    private GameObject bullet;
    private float cooldownRemaining = 0;
    private GazeInputFromAOI gazeInput;
    private float bulletImpulse = 20f;
    private bool inShooting = false;
    private float xAxisWithLimit;
    private float yAxisWithLimit;

    // Use this for initialization
	void Start () {
        gazeInput = gameObject.GetComponent<GazeInputFromAOI>();
        GameeventManager.onShootClickedHandler += reactOnEnableShoot;
	}

	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0) && inShooting && cooldownRemaining <= 0){
            Camera camera = Camera.main;
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, bulletFireRange))
            {
                Vector3 hitPoint = hitInfo.point;
                GameObject hittedGameObject = hitInfo.collider.gameObject;
                Debug.Log("hitted :" + hittedGameObject);
                //bullet = (GameObject) Instantiate(bulletPrefab, hitPoint, Quaternion.identity);
            }
          
           // bullet.rigidbody.AddForce(camera.transform.forward * bulletImpulse, ForceMode.Impulse);
        }
	}
    private void reactOnEnableShoot(int counter)
    {
        if (counter % 2 == 0)
        {
            inShooting = false;
        }
        else
        {
            inShooting = true;
        }
    }
}
