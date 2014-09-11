using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour
{
	void Start(){
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
				if (gameObject.tag == "Transmitter") {
						GameeventManager.transmitterDestroyd ();
				}
				Destroy (gameObject);
				if (gameObject.tag == "Refugee") {
					Debug.Log("HasHealth Destroy");
						networkView.RPC ("PlayerDied", RPCMode.All, "Test");
				}
		}
}
