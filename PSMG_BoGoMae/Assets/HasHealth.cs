using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {

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
        Debug.Log("in die");
        Destroy(gameObject);
    }
}
