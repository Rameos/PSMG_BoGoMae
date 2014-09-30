using UnityEngine;
using System.Collections;

public class RefugeeFPShooting : MonoBehaviour {

    private float cooldown = 1.0f;
    private int rocketsAmount = 0;
    private int rocketsPerItemPickUp = 10;
    public GameObject bulletPrefab;
    private Rect rocketsGUIposition = new Rect(100f, 50f, 150f, 50f);

    private float cooldownRemaining = 0f;
    private bool inShooting = false;
    private bool cooldownOver = true;
    private bool rocketsLeft = false;


    // Use this for initialization
	void Start () {
        GameeventManager.onEnableShootHandler += reactOnEnableShoot;
        GameeventManager.onDisableShootHandler += reactOnDisableShoot;
        GameeventManager.pickUpItemHandler += reactOnPickUpRocketLauncher;
	}


	
	// Update is called once per frame
	void Update () {

        CheckCooldown();
        CheckRocketsLeft();
        if(Input.GetMouseButtonDown(0) && inShooting && cooldownOver && rocketsLeft){
            cooldown = 1.0f;
            rocketsAmount--;
            Network.Instantiate(bulletPrefab, transform.FindChild(Config.REFUGEE_CAMERA).FindChild("rocketLauncher").transform.position, Camera.main.transform.rotation, 0);
        }
	}

    void OnGUI()
    {
        if (inShooting)
        {
            GUI.Box(rocketsGUIposition, "Raketen: " + rocketsAmount.ToString());
        }
    }

    private void CheckRocketsLeft()
    {
        if (rocketsAmount > 0)
        {
            rocketsLeft = true;
        }
        else
        {
            rocketsLeft = false;
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
        transform.FindChild(Config.REFUGEE_CAMERA).FindChild("rocketLauncher").transform.gameObject.SetActive(true);
        
    }

    private void reactOnDisableShoot()
    {
        inShooting = false;
        transform.FindChild(Config.REFUGEE_CAMERA).FindChild("rocketLauncher").transform.gameObject.SetActive(false);
    }

    private void reactOnPickUpRocketLauncher(int itemType)
    {
        if (itemType == Config.ROCKETLAUNCHER)
        {
            rocketsAmount += rocketsPerItemPickUp;
        }
    }

}
