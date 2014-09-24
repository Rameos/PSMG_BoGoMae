using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{

    private float bulletLifespan = 5.0f;
    private float bulletSpeed = 600f;
    private float bulletDamage = 50f;
    private float explosionRadius = 3f;
    private bool refugeeProtection = true;
    public GameObject fireEffect;

    private bool soundEnabled;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletLifespan -= Time.deltaTime;

        if (bulletLifespan <= 0)
        {
            Explode();
        }
        if (bulletLifespan <= 4.7)
        {
            refugeeProtection = false;
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
        if (!refugeeProtection)
        {
            Network.Instantiate(fireEffect, transform.position, Quaternion.identity, 0);
        }
  

        if (gameObjectTag == Config.TRANSMITTER_TAG)
        {
            addDamageToGameObject(gameObject);
        }
        else if (gameObjectTag == Config.REFUGEE_TAG)
        {
            if (refugeeProtection)
            {

            }
            else
            {
                addDamageToGameObject(gameObject);

            }
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
}
