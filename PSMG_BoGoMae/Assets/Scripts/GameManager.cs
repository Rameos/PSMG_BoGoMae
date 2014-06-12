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



    void ItemCollected(Item item)
    {

        itemText.text += " Speed (press (1) |"; 
        item.gameObject.audio.Play();
        Destroy(item.gameObject, item.gameObject.audio.clip.length);
        item.gameObject.renderer.enabled = false;

        Debug.Log("Speed Item aufgenommen!");
        

    }

 
}
