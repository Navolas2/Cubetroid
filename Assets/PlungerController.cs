using UnityEngine;
using System.Collections;

public class PlungerController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player") && !other.isTrigger) {
			this.GetComponent<Rigidbody2D> ().isKinematic = false;
		}
	}
}
