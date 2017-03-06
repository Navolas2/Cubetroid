using UnityEngine;
using System.Collections;

public class TargetReticle : MonoBehaviour {

	public GameObject target;
	private Transform center;
	//private float dist;

	// Use this for initialization
	void Start () {
		center = target.transform;
		//dist = FindDistance (center, this.transform);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveVertical = Input.GetAxis ("Vertical");
		if (moveVertical != 0) {
			//float angle = FindAngleToCenter (center, this.transform);
			//float rot = angle + (moveVertical);
			Vector3 N_pos = MoveObject (moveVertical);
			this.transform.position = N_pos;
		}
	}

	Vector3 MoveObject(float rot)
	{
		Vector2 shift = new Vector2 (this.transform.position.x - center.position.x, this.transform.position.y - center.position.y);
		float MoveXPrime = (shift.x * Mathf.Cos (rot)) - (shift.y * Mathf.Sin (rot));
		float MoveYPrime = (shift.x * Mathf.Sin (rot)) + (shift.y * Mathf.Cos (rot));
		Vector3 N_pos = new Vector3 (MoveXPrime + center.position.x , MoveYPrime + center.position.y);
		return N_pos;
	}

	float FindAngleToCenter(Transform g1, Transform g2){
		Vector3 ClungCenter = g1.position;
		Vector3 myCenter = g2.position;
		Vector3 direct = ClungCenter - myCenter;

		direct.z = 0;
		direct = direct.normalized;

		float circlerot = Mathf.Rad2Deg * Mathf.Acos(direct.y);
		if (direct.x > 0) {
			circlerot *= -1;
		}
		return Mathf.Deg2Rad * circlerot;
	}

	/*float FindDistance(Transform g1, Transform g2)
	{
		float N_X = g2.position.x - g1.position.x;
		float N_Y = g2.position.y - g1.position.y;
		N_X = N_X * N_X;
		N_Y = N_Y * N_Y;

		return Mathf.Sqrt (N_X + N_Y);
	}
*/
	public Vector2 GetPosition(){
		Vector2 shift = new Vector2 (this.transform.position.x - center.position.x, this.transform.position.y - center.position.y);
		shift.Normalize ();
		return shift;
	}
}
