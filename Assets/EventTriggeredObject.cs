using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventTriggeredObject : MonoBehaviour {

	private List<long> Triggers = new List<long>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Triggers.Count == 0) {
			this.gameObject.GetComponent<Trigger> ().Active_Trigger ();
		}
	}

	public void AddTrigger(long identification){
		Triggers.Add (identification);	
	}

	public void RemoveTrigger(long identification){
		Triggers.Remove (identification);
	}
}
