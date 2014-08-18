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
        Destroy(collider.gameObject);
        collider.gameObject.renderer.enabled = false;    

        if (collider.CompareTag("SpeedItem"))
        {
            Debug.Log("Collision with speeditem");
            GameeventManager.pickUpItem(Config.SPEED);
        }

        if (collider.CompareTag("ScopeItem"))
        {
            Debug.Log("Collision with scopeitem");
            GameeventManager.pickUpItem(Config.SCOPE);
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
    

