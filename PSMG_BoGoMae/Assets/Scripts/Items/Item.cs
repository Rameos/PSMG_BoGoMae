using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour {



	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); 
	}
}
