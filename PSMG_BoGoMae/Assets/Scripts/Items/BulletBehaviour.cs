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
        Debug.Log("rakete collision mit: " + collider.gameObject);
        Network.Instantiate(fireEffect, transform.position, Quaternion.identity, 0);
        Explode();

        soundEnabled = DroneController.soundEnabled;
        if (soundEnabled)
        //  if (true)
        {
            // besser in Rocket Explosion testen !!!!!!!!!!!!!!!!!
            AudioSource sound = GameObject.FindGameObjectWithTag("DroneCamera").GetComponent<AudioSource>();
            AudioClip audio = (AudioClip)(Resources.Load("Explosion 1"));
            sound.PlayOneShot(audio);
        }

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
                Debug.Log("in hasHealth()");
                hasHealth.ReceiveDamage(bulletDamage);
            }
        }
    }
}
