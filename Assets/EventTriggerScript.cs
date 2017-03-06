using UnityEngine;
using System.Collections;

public class EventTriggerScript : MonoBehaviour {

	// Use this for initialization
	public GameObject TriggeringObject;
	private long identification;

	void Start () {
		identification = Random.Range (int.MinValue, int.MaxValue) * Random.Range (int.MinValue, int.MaxValue);
		TriggeringObject.GetComponent<EventTriggeredObject> ().AddTrigger (identification);
		this.gameObject.GetComponent<Trigger> ().Active_Trigger = Triggered;
	}
	
	public void Triggered(){
		TriggeringObject.GetComponent<EventTriggeredObject> ().RemoveTrigger (identification);
	}
}
