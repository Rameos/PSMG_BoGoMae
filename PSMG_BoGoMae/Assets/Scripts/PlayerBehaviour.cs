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
        if (other.CompareTag("Item"))
        {

            Debug.Log("Collision");
            other.gameObject.SetActive(false);
            if (gameManager != null)
            {
                gameManager.SendMessage("ItemCollected", other.GetComponent<Item>());

            }
        }
    }
}
