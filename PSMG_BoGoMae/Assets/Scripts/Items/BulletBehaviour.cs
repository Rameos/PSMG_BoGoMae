using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    private float bulletLifespan = 5.0f;
    private float bulletSpeed = 400f;
    private float bulletDamage = 50f;
    private float explosionRadius = 3f;
    public GameObject fireEffect;

    private bool soundEnabled;

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
        string gameObjectTag = collider.gameObject.tag;
        GameObject gameObject = collider.gameObject;

        Network.Instantiate(fireEffect, transform.position, Quaternion.identity, 0);

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
        
        Explode();
        PlayExplosionSound();

    }

    private void PlayExplosionSound()
    {
        AudioSource sound = null;
        if (Network.isClient)
        {
            sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            soundEnabled = CameraController.soundEnabled;
        }
        else
        {
            sound = GameObject.FindGameObjectWithTag("DroneCamera").GetComponent<AudioSource>();
            soundEnabled = DroneController.soundEnabled;
        }

        if (soundEnabled)
        {
            AudioClip audio = (AudioClip)(Resources.Load("Explosion 1"));
            sound.PlayOneShot(audio);
        }
    }

    private void addDamageToGameObject(GameObject gameObject)
    {
        HasHealth hasHealth = gameObject.GetComponent<HasHealth>();
        hasHealth.ReceiveDamage(bulletDamage);
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
