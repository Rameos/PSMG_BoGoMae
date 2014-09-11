﻿using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    private float bulletLifespan = 5.0f;
    private float bulletSpeed = 400f;
    private float bulletDamage = 100f;
    private float explosionRadius = 3f;
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
        Network.Instantiate(fireEffect, transform.position, Quaternion.identity, 0);
        if (collider.gameObject.tag == Config.TRANSMITTER_TAG)
        {
            HasHealth hasHealth = collider.gameObject.GetComponent<HasHealth>();
            Debug.Log("hashHealt object: " + hasHealth);
            hasHealth.ReceiveDamage(bulletDamage);
        }
        Explode();

    }

    private void Explode()
    {
        Destroy(gameObject);
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("rakete collision mit: " + collision.gameObject);
        Network.Instantiate(fireEffect, collision.transform.position, Quaternion.identity, 0);
        Explode();

    }
    */

    /*
    private void Explode()
    {
        Destroy(gameObject);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        Debug.Log (colliders.Length);
        foreach (Collider collider in colliders)
        {
            HasHealth hasHealth = collider.GetComponent<HasHealth>();
			Debug.Log(hasHealth);
            if (hasHealth != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float damageRatio = 1f * (distance / explosionRadius);
                hasHealth.ReceiveDamage(bulletDamage * damageRatio);
            }
        }
    }*/
}
