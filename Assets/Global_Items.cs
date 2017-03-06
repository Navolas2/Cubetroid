using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Global_Items  : MonoBehaviour {

	public static Global_Items Items;

	public Rigidbody2D Health_PickUp;

	void Awake(){
		if (Items == null) {
			DontDestroyOnLoad (gameObject);
			Items = this;
		} else if (Items != this) {
			Destroy (gameObject);
		}
	}

}


