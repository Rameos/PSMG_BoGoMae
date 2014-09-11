using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{
		void Start ()
		{
		}

		public float hitPoints = 100f;

		public void ReceiveDamage (float damageAmount)
		{
				Debug.Log (this + "|| " + damageAmount);
				hitPoints -= damageAmount;
				if (hitPoints <= 0) {
						Die ();
				}
		}

		private void Die ()
		{
				if (gameObject.tag == Config.TRANSMITTER_TAG) {
						GameeventManager.transmitterDestroyd ();
				} else if (gameObject.tag == Config.REFUGEE_TAG) {
						Debug.Log ("gameObject: Refugee");
						GameeventManager.playerDied (gameObject);
				} else if (gameObject.tag == Config.DRONE_TAG) {
						Debug.Log ("gameObject: Drone");
						GameeventManager.playerDied (gameObject);
				}
		}
}
