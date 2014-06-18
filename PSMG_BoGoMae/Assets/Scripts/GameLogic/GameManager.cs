using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

    public GUIText itemText;


  
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}


    // receive message from PlayerCollisionResponse
    void ItemCollected(string itemName)
    {

        itemText.text += itemName; 

    }

 
}
