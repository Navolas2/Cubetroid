using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Vision : MonoBehaviour {

	public List<Room_Vision> Extended_Vision;
	private Renderer render;
	private bool in_room;
	public bool in_room_matters = true;
	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer> ();
	}

	public void Make_Visible(){
		if (!in_room) {
			render.enabled = true;
			foreach (Room_Vision r_v in Extended_Vision) {
				r_v.Make_Visible_Extend ();
			}
		} else if (!in_room_matters) {
			render.enabled = true;
			foreach (Room_Vision r_v in Extended_Vision) {
				r_v.Make_Visible_Extend ();
			}
		}
	}

	public void Make_Visible_Extend(){
		render.enabled = true;
	}

	public void Make_Invisible(){
		render.enabled = false;
		foreach (Room_Vision r_v in Extended_Vision) {
			r_v.Make_Invisible_Extend ();
		}
	}

	public void Make_Invisible_Extend(){
		render.enabled = false;
	}

	public void Put_in_Room(){
		in_room = true;
	}

	public void Remove_from_Room(){
		in_room = false;
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.tag == "Player") {
			in_room = true;
			Make_Invisible ();
			foreach (Room_Vision r_v in Extended_Vision) {
				r_v.Put_in_Room ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D obj){
		if (obj.tag == "Player") {
			in_room = false;
		}
	}
}
