using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Trigger : MonoBehaviour {

	public List<Door_Controller> connected_doors;
	public List<Room_Vision> connected_room;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D event_object){
		
		if (event_object.tag == "Player") {
			foreach (Door_Controller d_c in connected_doors) {
				d_c.Open_Door ();
			}
			foreach (Room_Vision r_v in connected_room) {
				r_v.Make_Invisible ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D event_object){
		if (event_object.tag == "Player") {
			foreach (Door_Controller d_c in connected_doors) {
				d_c.Close_Door ();
			}
			foreach (Room_Vision r_v in connected_room) {
				r_v.Make_Visible ();
			}
		}
	}
}
