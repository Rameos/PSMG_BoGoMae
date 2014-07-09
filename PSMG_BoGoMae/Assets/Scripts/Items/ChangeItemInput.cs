using UnityEngine;
using System.Collections;

public class ChangeItemInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameeventManager.pickUpItemHandler += reactOnChangedItem;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void reactOnChangedItem(int id)
    {
        guiText.text = "Fuck yeah:  " + id;
    }
}
