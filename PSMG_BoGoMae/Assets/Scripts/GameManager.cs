using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

    public GUIText itemText;
    
    private bool speedItemCollected = false;

  
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}



    void ItemCollected(Item item)
    {

        itemText.text += " Speed!"; 
        item.gameObject.audio.Play();
        Destroy(item.gameObject, item.gameObject.audio.clip.length);
        item.gameObject.renderer.enabled = false;
        speedItemCollected = true;

        Debug.Log("Speed Item aufgenommen!");
        

    }

 
}
