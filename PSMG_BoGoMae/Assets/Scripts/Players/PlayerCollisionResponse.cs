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
            GameeventManager.pickUpItem(1);
        }

        if (collider.CompareTag("ScopeItem"))
        {
            Debug.Log("Collision with scopeitem");
            GameeventManager.pickUpItem(0);
        }
    }

    void ItemCollected(string blablabla)
    {
        Debug.Log("Test");
    }
}
    

