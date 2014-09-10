using UnityEngine;
using System.Collections;

public class DroneRocketAttack : MonoBehaviour {

    private float cooldown = 1.0f;
    private float energyRocketCosts = 40f;
    private int EnergyAmount = 10;
    private int rocketsPerItemPickUp = 10;
    public GameObject bulletPrefab;
    private Rect rocketsGUIposition = new Rect(100f, 50f, 150f, 50f);

    private float cooldownRemaining = 0f;
    private bool inShooting = false;
    private bool cooldownOver = true;
    private bool energyLeft = false;
    private Camera droneCamera;
    private Vector3 spawnPointRocketleft;
    private Vector3 spawnPointRocketRight;
    private Energymanagement energymanagment;


    // Use this for initialization
    void Start()
    {
        energymanagment = GetComponent<Energymanagement>();
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;
        droneCamera = GameObject.FindGameObjectWithTag(Config.DRONE_CAMERA_TAG).camera;

    }


    // Update is called once per frame
    void Update()
    {
        CheckCooldown();
        CheckEnergyLeft();
        spawnPointRocketleft = droneCamera.transform.position + new Vector3(-20f, 0f, 0f);
        spawnPointRocketRight = droneCamera.transform.position + new Vector3(20f, 0f, 0f);
        if (Input.GetMouseButtonDown(0) && inShooting && cooldownOver && energyLeft)
        {
            cooldown = 1.0f;
            energymanagment.Energy = energymanagment.Energy - energyRocketCosts;
            Network.Instantiate(bulletPrefab, spawnPointRocketleft, droneCamera.transform.rotation, 0);
            Network.Instantiate(bulletPrefab, spawnPointRocketRight, droneCamera.transform.rotation, 0);
        }
    }
    void OnGUI()
    {
        if (inShooting)
        {
            GUI.Box(rocketsGUIposition, "Raketen: " + EnergyAmount.ToString());
        }
    }

    private void CheckEnergyLeft()
    {
        if (energymanagment.Energy > energyRocketCosts)
        {
            energyLeft = true;
        }
        else
        {
            energyLeft = false;
        }
    }

    private void CheckCooldown()
    {
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
            cooldownOver = false;

        }
        else
        {
            cooldownOver = true;
        }

        //Debug.Log(cooldown + "|| " +cooldownOver);
    }

    private void reactOnEnableShoot()
    {
        inShooting = true;

    }

    private void reactOnDisableShoot()
    {
        inShooting = false;
    }

    private void reactOnPickUpRocketLauncher(int itemType)
    {
        if (itemType == Config.ROCKETLAUNCHER)
        {
            EnergyAmount += rocketsPerItemPickUp;
        }
    }
}
