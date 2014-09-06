using UnityEngine;
using System.Collections;

public class PlayerCollisionResponse : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Goal"))
        {
            Debug.Log("Ziel erreicht, Flüchtling gewinnt");
            
        }

        if (collider.CompareTag("RocketLauncherItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;
            Debug.Log("Collision with rocketlauncher");
            GameeventManager.pickUpItem(Config.ROCKETLAUNCHER);
        }

        if (collider.CompareTag("FernglasItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;
            Debug.Log("Collision with fernglas");
            GameeventManager.pickUpItem(Config.FERNGLAS);
        }

        if (collider.CompareTag("SpeedItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;    
            Debug.Log("Collision with speeditem");
            GameeventManager.pickUpItem(Config.SPEED);
        }

        if (collider.CompareTag("Transmitter"))
        {
            Debug.Log("Collision with transmitter");
            GameeventManager.transmitterCollected();
        }
    }

    void ItemCollected(string blablabla)
    {
        Debug.Log("Test");
    }
}
    

