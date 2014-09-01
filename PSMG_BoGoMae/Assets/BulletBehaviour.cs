using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    private float bulletLifespan = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bulletLifespan -= Time.deltaTime;

        if (bulletLifespan <= 0)
        {
            Explode();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
       
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
