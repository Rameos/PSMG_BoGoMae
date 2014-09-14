using UnityEngine;
using System.Collections;

public class FlameLife : MonoBehaviour {

    private float flameLife = 2f;
    private float bulletDamage = 50f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        flameLife -= Time.deltaTime;
        if (flameLife <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        string gameObjectTag = collider.gameObject.tag;
        GameObject gameObject = collider.gameObject;


        if (gameObjectTag == Config.TRANSMITTER_TAG)
        {
            addDamageToGameObject(gameObject);
        }
        else if (gameObjectTag == Config.REFUGEE_TAG)
        {
            addDamageToGameObject(gameObject);
        }
        else if (gameObjectTag == Config.DRONE_TAG)
        {
            addDamageToGameObject(gameObject);
        }
    }

    private void addDamageToGameObject(GameObject gameObject)
    {
        HasHealth hasHealth = gameObject.GetComponent<HasHealth>();
        hasHealth.ReceiveDamage(bulletDamage);
    }
}
