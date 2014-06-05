using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start () {

        gameManager = FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedItem"))
        {

            Debug.Log("Collision with speeditem");
            other.gameObject.SetActive(false);
            if (gameManager != null)
            {

                gameManager.SendMessage("ItemCollected", other.GetComponent<Item>());
                gameManager.SendMessage("SpeedUp", 5);

            }
        }
        if (other.CompareTag("ScopeItem"))
        {

            Debug.Log("Collision with scopeitem");
            other.gameObject.SetActive(false);
            if (gameManager != null)
            {
                Debug.Log("in gamemanager != null");
                gameManager.SendMessage("ItemCollected", other.GetComponent<Item>());
                gameManager.SendMessage("PickedUpScope");

            }
        }
    }
}
