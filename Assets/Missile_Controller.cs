using UnityEngine;
using System.Collections;

public class Missile_Controller : MonoBehaviour {

	private Rigidbody2D rb2d;
	private Collider2D myBody;
	private float Speed;
	private float currentRotation;
	private float rotation;
	private GameObject target;
	private Vector2 CurVel;
	private float prevDir;
	private int launchCount;
	public int turnRadius = 10;

	public void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		myBody = GetComponent<Collider2D> ();
		myBody.enabled = false;
	}

	public void Launch (float startSpeed, GameObject tar, float curRot)	{
		target = tar;
		Speed = startSpeed;
		currentRotation = curRot;
		prevDir = 1;
		launchCount = 10;
		//rotation = 360;
		Vector2 t = Vector2.right;
		t = RotateVector (t, Mathf.Deg2Rad * curRot);
		CurVel = new Vector2 (startSpeed * t.x, startSpeed * t.y);
		rb2d.velocity = CurVel;
	}

	void FixedUpdate(){
		if (launchCount != 0) {
			launchCount--;
		} else {
			myBody.enabled = true;
		}
		float angle = calcAngle ();
		if (angle > 1 || angle < -1) {
			rotation = angle;
		}

		if (rotation > turnRadius) {
			this.transform.Rotate (0f, 0f, turnRadius, Space.Self);

			Vector2 t = Vector2.right;
			currentRotation -= turnRadius;
			t = RotateVector (t, Mathf.Deg2Rad * (currentRotation));
			CurVel = new Vector2 (Speed * t.x, Speed * t.y);
			rotation -= turnRadius;
		} else if (rotation < -1 * turnRadius) {
			this.transform.Rotate (0f, 0f, -1 * turnRadius, Space.Self);
			Vector2 t = Vector2.right;
			currentRotation += turnRadius;
			t = RotateVector (t, Mathf.Deg2Rad * (currentRotation));
			CurVel = new Vector2 (Speed * t.x, Speed * t.y);
			rotation += turnRadius;
		}
		else if(rotation != 0) {
			this.transform.Rotate (0f, 0f, rotation, Space.Self);
			Vector2 t = Vector2.right;
			currentRotation -= rotation;
			t = RotateVector (t, Mathf.Deg2Rad * (currentRotation));

			CurVel = new Vector2 (Speed * t.x, Speed * t.y);
			rotation = 0;
		}
		rb2d.velocity = CurVel;
	}

	Vector2 RotateVector(Vector2 t, float curRot){
		float MoveXPrime = (t.x * Mathf.Cos(curRot)) - (t.y * Mathf.Sin (curRot));
		float MoveYPrime = (t.x * Mathf.Sin(curRot)) + (t.y * Mathf.Cos (curRot));
		return new Vector2 (MoveXPrime, MoveYPrime);
	}

	float calcAngle(){
		float xLoc = target.transform.position.x - this.transform.position.x;
		float yLoc = target.transform.position.y - this.transform.position.y;
		Vector2 loc = new Vector2 (xLoc, yLoc);
		loc.Normalize ();
		Vector2 myLoc = CurVel;
		myLoc.Normalize ();
		float dir1 = Mathf.Atan2(loc.y, loc.x);
		float dir2 = Mathf.Atan2(myLoc.y, myLoc.x) ;

		float direction = dir1 - dir2;
		if (dir1 * Mathf.Rad2Deg > 150 || -150 > dir1 * Mathf.Rad2Deg) {
			direction = prevDir;
		} else {
			prevDir = direction;
		}
		float result = Mathf.Acos (loc.x * myLoc.x + loc.y * myLoc.y);
		float retValue = result * Mathf.Rad2Deg;

		if (direction > 0) {
			return -1 * retValue;
		} else {
			return retValue;
		}
	
		//return 180 - result * Mathf.Rad2Deg;
	}


	void OnTriggerEnter2D(Collider2D other)
	{if (launchCount == 0) {
			if (other.gameObject.GetComponent<Rigidbody2D> () != null) {
				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (CurVel.x / 2, CurVel.y / 2);
			}
			if (!other.isTrigger) {
				Destroy (this.gameObject);
			}
		}
	}
}


