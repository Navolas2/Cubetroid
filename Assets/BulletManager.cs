using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

	private CloneManager clone_Manager;
	private Rigidbody2D rb2d;
	private int AttractionCount;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		clone_Manager = GetComponent<CloneManager> ();
		AttractionCount = 300;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (AttractionCount != 0) {
			AttractionCount--;
		} else {
			Vector2 movement = SplitAttraction (new Vector2());
			rb2d.AddForce (movement);
		}
	}

	Vector2 SplitAttraction(Vector2 movement)
	{
		Vector2 outputVec = new Vector2 ();
		//parent.GetClone().GetCloneList ();
		List<Rigidbody2D> clone_List = clone_Manager.CloneList;
		int MyIndex = clone_List.IndexOf(this.rb2d);
		for (int i = 0; i < clone_List.Count; i++) {
			if (MyIndex != i) {
				Vector2 hp = clone_List [i].gameObject.GetComponent<CloneManager>().GetHealth();
				float angle = ForceManager.FindAngleToCenter (clone_List [i].gameObject, clone_List [MyIndex].gameObject);
				float xPrime = -1 * (hp.x / 10f) * Mathf.Sin (angle);
				float yPrime = (hp.x / 10f) * Mathf.Cos (angle);
				outputVec.x += xPrime;
				outputVec.y += yPrime;
			}
		}

		movement.x += outputVec.x;
		movement.y += outputVec.y;
		return movement;
	}
}
