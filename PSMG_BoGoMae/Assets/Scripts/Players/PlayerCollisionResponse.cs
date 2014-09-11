using UnityEngine;
using System.Collections;

public class PlayerCollisionResponse : MonoBehaviour
{
    private bool soundEnabled;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void PlayItemCollectedSound()
    {
        soundEnabled = CameraController.soundEnabled;
        if (soundEnabled)
        {
            AudioSource sound = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            AudioClip audio = (AudioClip)(Resources.Load("Item aufheben"));
            sound.PlayOneShot(audio);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Goal"))
        {
            Debug.Log("collision with goal");
            GameeventManager.onGoalReached();
            
        }

        if (collider.CompareTag("RocketLauncherItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;
            GameeventManager.pickUpItem(Config.ROCKETLAUNCHER);
            PlayItemCollectedSound();
        }

        if (collider.CompareTag("FernglasItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;
            GameeventManager.pickUpItem(Config.FERNGLAS);
            PlayItemCollectedSound();
        }

        if (collider.CompareTag("SpeedItem"))
        {
            Destroy(collider.gameObject.light);
            Destroy(collider.gameObject);
            collider.gameObject.renderer.enabled = false;    
            GameeventManager.pickUpItem(Config.SPEED);
            PlayItemCollectedSound();
        }
    }

}
    

