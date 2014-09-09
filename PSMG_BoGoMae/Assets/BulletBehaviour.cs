using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    private float bulletLifespan = 5.0f;
    public float bulletSpeed = 30f;
    public float bulletDamage = 100f;
    public float explosionRadius = 3f;
    public GameObject fireEffect;

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

    void FixedUpdate()
    {
        MoveBulletForward();
    }

    private void MoveBulletForward()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime, Space.World);
    }
    
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("rakete collision mit: " + collider.gameObject);
        Network.Instantiate(fireEffect, transform.position, Quaternion.identity, 0);
        Explode();

    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("rakete collision mit: " + collision.gameObject);
        Network.Instantiate(fireEffect, collision.transform.position, Quaternion.identity, 0);
        Explode();

    }
    */
    private void Explode()
    {
        Destroy(gameObject);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            HasHealth hasHealth = collider.GetComponent<HasHealth>();
            if (hasHealth != null)
            {
                hasHealth.ReceiveDamage(bulletDamage);
            }
        }
    }
}
