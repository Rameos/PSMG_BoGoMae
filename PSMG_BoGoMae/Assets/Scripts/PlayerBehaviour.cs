using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private GameManager gameManager;
    public GUIText itemText;
	// Use this for initialization
	void Start () {

        gameManager = FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("UseSpeed"))
        {
            gameObject.SendMessage("SpeedUp");
        }

        if (Input.GetButton("UseScope"))
        {
            gameObject.SendMessage("PickedUpScope");
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedItem"))
        {

            Debug.Log("Collision with speeditem");
            other.gameObject.SetActive(false);
            if (gameManager != null)
            {
                itemText.text += " Speed (press (1) |"; 


            }
        }
        if (other.CompareTag("ScopeItem"))
        {

            Debug.Log("Collision with scopeitem");
            other.gameObject.SetActive(false);
            if (gameManager != null)
            {
                itemText.text += " Scope (press (2)"; 

            }
        }
    }



}
     

