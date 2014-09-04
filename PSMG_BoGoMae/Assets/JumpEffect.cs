using UnityEngine;
using System.Collections;

public class JumpEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision ");
        GameObject refugee = collider.transform.gameObject;
        refugee.transform.Translate(new Vector3(refugee.transform.position.x + 80, refugee.transform.position.y + 80, refugee.transform.position.z) * 10f * Time.deltaTime, Space.World);
    }


}
