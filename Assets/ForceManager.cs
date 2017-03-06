using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceManager : MonoBehaviour {

	private int CurJumps;
	private int WallJump;
	private int WallDir;
	private GameObject clungWall;
	private int MaxJumps;
	private bool onGround;
	private bool ClingMode;
	private bool Clinging;

	public float speed = 10;
	public float jumpPower = 7;

	private Rigidbody2D rb2d;

	private PlayerControllerVersion2 parent;
	// Use this for initialization
	void Start () {
		WallDir = -1;

		onGround = true;

		rb2d = GetComponent<Rigidbody2D> ();
		SetStats ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal") * speed;
		float moveVertical = 0;
		if (Input.GetKeyDown (KeyCode.Space)  && PlayerUpgrades.upgrades.jump) {
			Vector2 curVel = rb2d.velocity;
			if (CurJumps > 0 || WallJump > 0) {
				if (WallJump > 0) {
					WallJump--;
					curVel.y = jumpPower;
					if (WallDir == 2) {
						curVel.x = jumpPower * -1;
					} else if (WallDir == 4) {
						curVel.x = jumpPower;
					} else if (WallDir == 1) {
						curVel.y = jumpPower * -1;
					} else if (WallDir == 0) {
						float rot = Mathf.Deg2Rad * (180 + (Mathf.Rad2Deg * FindAngleToCenter (clungWall, this.gameObject)));
						float xPrime = -1 * jumpPower * Mathf.Sin (rot);
						float yPrime = jumpPower * Mathf.Cos (rot);
						curVel.y = yPrime;
						curVel.x = xPrime;
					}
				} else {
					CurJumps--;
					curVel.y = jumpPower;
				}
				onGround = false;
				parent.Camera.jumpsUpdate (CurJumps, MaxJumps);
				rb2d.velocity = curVel;
			}
		}
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		movement = ApplyWallGravity (movement);
		//Toggle?
		movement = SplitAttraction (movement);
		rb2d.AddForce (movement);
	}

	public static float FindAngleToCenter(GameObject g1, GameObject g2){
		Vector3 ClungCenter = g1.transform.position;
		Vector3 myCenter = g2.transform.position;
		Vector3 direct = ClungCenter - myCenter;

		direct.z = 0;
		direct = direct.normalized;

		float circlerot = Mathf.Rad2Deg * Mathf.Acos(direct.y);
		if (direct.x > 0) {
			circlerot *= -1;
		}
		return Mathf.Deg2Rad * circlerot;
	}

	public Vector2 SplitAttraction(Vector2 movement)
	{
		Vector2 outputVec = new Vector2 ();
		List<Rigidbody2D> clone_List = parent.Clone.CloneList;
		int MyIndex = clone_List.IndexOf(this.rb2d);
		for (int i = 0; i < clone_List.Count; i++) {
			if (MyIndex != i) {
				Vector2 hp  = clone_List [i].gameObject.GetComponent<CloneManager>().GetHealth();

				float angle = FindAngleToCenter (clone_List [i].gameObject, clone_List [MyIndex].gameObject);
				float xPrime = -1 * (hp.x / 12f) * Mathf.Sin (angle);
				float yPrime = (hp.x / 10f) * Mathf.Cos (angle);
				outputVec.x += xPrime;
				outputVec.y += yPrime;
			}
		}

		movement.x += outputVec.x;
		movement.y += outputVec.y;
		return movement;
	}

	Vector2 ApplyWallGravity (Vector2 movement)
	{
		if(WallDir != -1 && Clinging && ClingMode){
			float gravityCounter = 9.8f;

			float rot = 0f;

			if (WallDir == 2) {
				rot = Mathf.Deg2Rad * (clungWall.transform.rotation.eulerAngles.z * -1);
			} else if (WallDir == 4 || WallDir == 1) {
				rot = Mathf.Deg2Rad * (clungWall.transform.rotation.eulerAngles.z);
			} else if (WallDir == 0) {
				rot = FindAngleToCenter(clungWall, this.gameObject);

			} else {
				rot = 0;
			}
			//float xPrime = (movement.x * Mathf.Cos(rot)) - (movement.y * Mathf.Sin (rot));
			//float yPrime = (movement.x * Mathf.Sin(rot)) + (movement.y * Mathf.Cos (rot));
			float xPrime = -1 * 9.8f * Mathf.Sin (rot);
			float yPrime = 9.8f * Mathf.Cos (rot);

			if (WallDir == 2) {
				rot = Mathf.Deg2Rad * (clungWall.transform.rotation.eulerAngles.z);
			} else if (WallDir == 4 || WallDir == 1) {
				rot = Mathf.Deg2Rad * (clungWall.transform.rotation.eulerAngles.z * -1);
			} else if (WallDir == 0) {
				if (rot > Mathf.PI * .75f || rot < Mathf.PI * -.75f || rot > Mathf.PI * -.25f || rot < Mathf.PI * .25f) {
					rot = 0f;
				} else {
					rot = Mathf.PI * .5f;
				}
			}
			float MoveXPrime = (movement.x * Mathf.Cos(rot)) - (movement.y * Mathf.Sin (rot));
			float MoveYPrime = (movement.x * Mathf.Sin(rot)) + (movement.y * Mathf.Cos (rot));

			movement.x = MoveXPrime + xPrime;
			movement.y = MoveYPrime + yPrime + gravityCounter;
			return movement;
		}
		return movement;
	}

	void Grounded()
	{
		onGround = true;
		CurJumps = MaxJumps;
		WallJump = 0;
		WallDir = -1;
		parent.Camera.jumpsUpdate (CurJumps, MaxJumps);
	}

	void AddWallJumps()
	{
		if (WallJump == 0) {
			WallJump++;
			parent.Camera.jumpsUpdate (CurJumps + WallJump, MaxJumps);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground")) {
			Grounded ();
		} else if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag("Player_Bullet")) {
			//MergeParts (other.gameObject);
			parent.Clone.MergeParts (other.gameObject);
		} else if (other.gameObject.CompareTag ("Cling")) {
			if (other.isTrigger) {
				if (parent.ClingCheck (other) && ClingMode == true) {
					Clinging = true;
					clungWall = other.gameObject;
				}
				if (other.gameObject.transform.GetChild (0).CompareTag ("Wall")) {
					if (this.gameObject.transform.position.x > other.gameObject.transform.position.x) {
						if (Clinging) {
							WallDir = 4;
						}
					} else {
						if (Clinging) {
							WallDir = 2;
						}
					}
					AddWallJumps ();
				} else if (other.gameObject.transform.GetChild (0).CompareTag ("Roof")) {
					if (this.gameObject.transform.position.y > other.gameObject.transform.position.y) {
						Grounded ();
					} else {
						if (Clinging) {
							WallDir = 1;
							AddWallJumps ();
						}
					}
				} else if (other.gameObject.transform.GetChild (0).CompareTag ("Circle")) {
					if (Clinging) {
						WallDir = 0;
						AddWallJumps ();
					}

				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ground")) {
			onGround = false;
		}else if (other.gameObject.CompareTag ("Cling")) {
			if (other.isTrigger) {
				if (other.gameObject == clungWall) {
					Clinging = false;
					WallJump = 0;
					parent.Camera.jumpsUpdate (CurJumps, MaxJumps);
				}
			}
		}
	}

	public void UpdateJumps()
	{
		MaxJumps = PlayerUpgrades.upgrades.jumps;
		if (onGround) {
			CurJumps = MaxJumps;
		}
	}

	/************************************/
	//GETTERS AND SETTERS
	/************************************/

	public bool _ClingMode
	{
		get{ return ClingMode; }
		set{ ClingMode = value; }
	}

	public bool _Clinging
	{
		get{ return Clinging; }
	}

	public bool IsOnGround()
	{
		return onGround;
	}

	public Vector2 _Jumps
	{
		get{ return new Vector2 (CurJumps, MaxJumps); }
	}

	public PlayerControllerVersion2 _Parent
	{
		set { parent = value; }
	}

	void SetStats()
	{
		CurJumps = 3;
		MaxJumps = 3;
	}
}
