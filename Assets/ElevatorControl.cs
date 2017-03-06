using UnityEngine;
using System.Collections;

public class ElevatorControl : MonoBehaviour {

	public float moveDist = 100f;
	public float slowDist = 0f;
	public int moveDirection = 1; //1 (up), 2 (right), -1(down), -2(left)
	public float moveSpeed = 10f;
	public int waitTime = 10;
	public bool loop = true;
	public bool locked = false;

	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 lastPos;
	private int lastPosition;
	private int curwait;
	private float Zrot;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		Vector2 toEnd;
		if (moveDirection == 1 || moveDirection == -1) {
			toEnd = CalcVelocity (0, moveDist * moveDirection);
		} else {
			toEnd = CalcVelocity (moveDist * (moveDirection / 2), 0);
		}
		endPos = this.transform.position;
		endPos.x += toEnd.x;
		endPos.y += toEnd.y;
		Zrot = this.transform.localRotation.eulerAngles.z;
		rb2d = this.GetComponent<Rigidbody2D> ();

		lastPos = startPos;
		lastPosition = 1;

		curwait = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 curLoc = this.transform.position;
		float curSpeed = moveSpeed;
		float distTravel = DistanceMoved (curLoc, lastPos);
		if (curwait == 0 && !locked) {
			if (moveDirection == 1 || moveDirection == 2) {
				if (distTravel < moveDist) {
					if (distTravel + slowDist >= moveDist) {
						//slow down
						curSpeed = curSpeed / 2;
					}
					if (moveDirection == 1) {
						if (Zrot == 0) {
							rb2d.velocity = new Vector2 (0, curSpeed);
						} else {
							rb2d.velocity = CalcVelocity (0, curSpeed);
						}
					} else {
						if (Zrot == 0) {
							rb2d.velocity = new Vector2 (curSpeed, 0);
						} else {
							rb2d.velocity = CalcVelocity (curSpeed, 0);
						}	
					}
				} else {
					rb2d.velocity = new Vector2 ();
					curwait = waitTime;
					if (loop) {
						moveDirection *= -1;
						if (lastPosition > 0) {
							lastPos = endPos;
							lastPosition *= -1;
						} else {
							lastPos = startPos;
							lastPosition *= -1;
						}
					}
				}
			} else if (moveDirection == -1 || moveDirection == -2) {
				if (distTravel < moveDist) {
					if (distTravel + slowDist >= moveDist) {
						//slow down
						curSpeed = curSpeed / 2;
					}
					if (moveDirection == -1) {
						if (Zrot == 0) {
							rb2d.velocity = new Vector2 (0, curSpeed * -1);
						} else {
							rb2d.velocity = CalcVelocity (0, curSpeed * -1);
						}
					} else {
						if (Zrot == 0) {
							rb2d.velocity = new Vector2 (curSpeed * -1, 0);
						} else {
							rb2d.velocity = CalcVelocity (curSpeed * -1, 0);
						}	
					}
				} else {
					rb2d.velocity = new Vector2 ();
					curwait = waitTime;
					if (loop) {
						moveDirection *= -1;
						if (lastPosition > 0) {
							lastPos = endPos;
							lastPosition *= -1;
						} else {
							lastPos = startPos;
							lastPosition *= -1;
						}
					}
				}
			}
		} else if (curwait > 0) {
				curwait--;
		} else if(locked){
			rb2d.velocity = new Vector2 ();
		}
	} 

	private float DistanceMoved (Vector3 curLoc, Vector3 startingPosition){
		float xVal = curLoc.x - startingPosition.x;
		xVal = Mathf.Pow (xVal, 2);

		float yVal = curLoc.y - startingPosition.y;
		yVal = Mathf.Pow (yVal, 2);

		float dist = Mathf.Sqrt (xVal + yVal);
		return dist;

	}

	private Vector2 CalcVelocity (float xSpeed, float ySpeed){
		float MoveXPrime = (xSpeed * Mathf.Cos(Mathf.Deg2Rad * Zrot)) - (ySpeed * Mathf.Sin (Mathf.Deg2Rad * Zrot));
		float MoveYPrime = (xSpeed * Mathf.Sin(Mathf.Deg2Rad * Zrot)) + (ySpeed * Mathf.Cos (Mathf.Deg2Rad * Zrot));
		return new Vector2 (MoveXPrime, MoveYPrime);
	}

}
