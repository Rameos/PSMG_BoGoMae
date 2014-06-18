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
        
        if (Input.GetButton("UseScope"))
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
            gameObject.SendMessage("PickedUpSpeed");
            gameObject.SendMessage("ItemCollected", " SpeedUp  ");

        }

        if (collider.CompareTag("ScopeItem"))
        {

            Debug.Log("Collision with scopeitem");

            gameObject.SendMessage("ItemCollected", "| Scope (2) |");

        }
    }

}
    

