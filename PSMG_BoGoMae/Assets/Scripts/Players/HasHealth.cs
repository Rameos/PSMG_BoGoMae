using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{


    private float health = 100f;
    private float maxHealth = 100f;
    private float healthIncreaseFactor = 0.5f;

    void Start()
    {
    }

    void Update()
    {
        if (health < maxHealth)
        {
            health += healthIncreaseFactor * Time.deltaTime;
            
        }
    }

    public void ReceiveDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.tag == Config.TRANSMITTER_TAG)
        {
            GameeventManager.transmitterDestroyd();
            Destroy(gameObject);
        }
        else if (gameObject.tag == Config.REFUGEE_TAG)
        {
            GameeventManager.playerDied(gameObject);
        }
        else if (gameObject.tag == Config.DRONE_TAG)
        {
            GameeventManager.playerDied(gameObject);
        }
    }

    void OnGUI()
    {
		if (gameObject.tag != Config.TRANSMITTER_TAG && networkView.isMine) {
            GUIStyle font = new GUIStyle();
            font.fontSize = 15;
            font.fontStyle = FontStyle.Normal;
            font.normal.textColor = new Color32(50, 50, 50, 150);
						GUI.Label (new Rect (Screen.width - 200, 10, 100, 25), "Health:" + Mathf.Round (health) + "/" + maxHealth,font);
				}
    }
}
