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
            Debug.Log("collision with goal");
            GameeventManager.onGoalReached();

        }

        if (collider.CompareTag("RocketLauncherItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;
            GameeventManager.pickUpItem(Config.ROCKETLAUNCHER);
        }

        if (collider.CompareTag("FernglasItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;
            GameeventManager.pickUpItem(Config.FERNGLAS);
        }

        if (collider.CompareTag("SpeedItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;    
            GameeventManager.pickUpItem(Config.SPEED);
        }
    }

}
    

