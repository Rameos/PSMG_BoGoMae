using UnityEngine;
using System.Collections;

public class DroneRocketAttack : MonoBehaviour {

    public float cooldown = 1f;
    public GameObject bulletPrefab;

    private float cooldownRemaining = 0;
    private bool inShooting = false;


    // Use this for initialization
    void Start()
    {
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && inShooting && cooldownRemaining <= 0)
        {
            Network.Instantiate(bulletPrefab, transform.FindChild(Config.DRONE_CAMERA).position, Camera.main.transform.rotation, 0);
        }
    }
    private void reactOnEnableShoot()
    {
        Debug.Log("in drone rocket attack true");
        inShooting = true;

    }

    private void reactOnDisableShoot()
    {

        inShooting = false;
    }
}
