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
        
        if (Input.GetButtonDown("UseScope"))
        {
            gameObject.SendMessage("UsedScope");
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
        collider.gameObject.renderer.enabled = false;    

        if (collider.CompareTag("SpeedItem"))
        {

            Debug.Log("Collision with speeditem");
            GameeventManager.pickUpItem(0);
            //gameObject.SendMessage("PickedUpSpeed");
            //gameObject.SendMessage("ItemCollected", " SpeedUp  ");

        }

        if (collider.CompareTag("ScopeItem"))
        {

            Debug.Log("Collision with scopeitem");
            GameeventManager.pickUpItem(1);
            
            //gameObject.SendMessage("ItemCollected", "| Scope (2) |");

        }
    }

    void ItemCollected(string blablabla)
    {
        Debug.Log("Test");
    }
}
    

