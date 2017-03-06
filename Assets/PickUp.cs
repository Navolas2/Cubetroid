using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	private int startUp_Count = 10;
	// Use this for initialization
	void Start () {
	}

	void FixedUpdate(){
		if (startUp_Count > 0) {
			startUp_Count--;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player") && startUp_Count == 0) {
			print ("Collected");
			Destroy (this.gameObject);
		}
	}
}
