﻿using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{
    void Start()
    {
    }

    public float hitPoints = 100f;

    public void ReceiveDamage(float damageAmount)
    {
        Debug.Log(this + "|| " + damageAmount);
        hitPoints -= damageAmount;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        if (gameObject.tag == Config.TRANSMITTER_TAG)
        {
            GameeventManager.transmitterDestroyd();
        }

        if (gameObject.tag == Config.REFUGEE_TAG)
        {
            networkView.RPC("PlayerDied", RPCMode.All, "Test");
        }
    }

	[RPC]
	public void PlayerDied(string message){
		Debug.Log (message + " isClient: " + Network.isClient + " isServer: " + Network.isServer);

	}

}
