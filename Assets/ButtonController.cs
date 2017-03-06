using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour, Trigger {

	private Global_Delegates.TriggerFunction Trigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Button_Plunger")) {
			other.GetComponent<Rigidbody2D> ().isKinematic = true;
			Trigger ();
		}
	}

	//Interface Implementation

	public Global_Delegates.TriggerFunction Active_Trigger{

		get{return Trigger;} 

		set{Trigger = value;}

	}
}
