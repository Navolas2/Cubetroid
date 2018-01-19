using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceManager : MonoBehaviour {

	private int CurJumps;
	private int WallJump;
	private int WallDir;
	private int JumpDir;
	private GameObject clungWall;
	private int MaxJumps;
	private bool onGround;
	private List<GameObject> current_ground;
	private bool ClingMode;
	private bool Clinging;
	private bool limit_breaker = false;

	public float jumpPower = 7;
	public float accel_speed = 10;
	[Space(15)]
	public float max_cube_ground_speed = 20;
	public float max_cube_air_speed = 20;
	[Space(10)]
	public float max_sphere_ground_speed = 40;
	public float max_sphere_air_speed = 40;
	[Space(10)]
	public float max_pyramid_ground_speed = 25;
	public float max_pyramid_air_speed = 25;
	[Space(10)]
	public float max_cube_dense_ground_speed = 20;
	public float max_cube_dense_air_speed = 20;
	[Space(20)]
	public bool Testing = false;
	private Rigidbody2D rb2d;

	private PlayerControllerVersion2 parent;
	// Use this for initialization
	void Start () {
		WallDir = -1;
		JumpDir = -1;

		onGround = false;
		current_ground = new List<GameObject> ();
		rb2d = GetComponent<Rigidbody2D> ();
		UpdateJumps ();

		//parent.Camera.jumpsUpdate (CurJumps + "", MaxJumps);
		//SetStats ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!parent.cam.Active) {
			float moveHorizontal = Input.GetAxis ("Horizontal") * accel_speed;
			float moveVertical = 0;
			if (Input.GetButtonDown ("Unlimited") && PlayerUpgrades.upgrades.sphere) {
				limit_breaker = !limit_breaker;
				AudioManager.Singleton.PlaySound ("LimitBreak");
				if (limit_breaker) {
					parent.Camera.jumpsUpdate ("---", MaxJumps);
				} else {
					parent.Camera.jumpsUpdate (CurJumps + "", MaxJumps);
				}
			}
			if (Input.GetButtonDown ("Jump") && PlayerUpgrades.upgrades.jump && !limit_breaker) {
				AudioManager.Singleton.PlaySound ("Jump");
				Vector2 curVel = rb2d.velocity;
				if (CurJumps > 0 || WallJump > 0) {
					if (WallJump > 0) {
						WallJump--;
						curVel.y = jumpPower;
						if (JumpDir == 4 && WallDir == 2) {
							curVel.x = jumpPower * -1;
						} else if (JumpDir == 2 && WallDir == 2) {
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
						if (!Testing) {
							CurJumps--;
						}
						curVel.y = jumpPower;
					}
					onGround = false;
					current_ground.Clear ();
					parent.Camera.jumpsUpdate (CurJumps + "", MaxJumps);
					rb2d.velocity = curVel;
				}
			}
			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
			movement = ApplyWallGravity (movement);
			//Toggle?
			movement = SplitAttraction (movement);
			rb2d.AddForce (movement);
			Vector2 vel = rb2d.velocity;
			if (!limit_breaker) {
				switch (parent.Form) {
				case 0:
					if (onGround || Clinging) {
						if (vel.x > max_cube_ground_speed) {
							vel.x = max_cube_ground_speed;
						}
						if (vel.y > max_cube_ground_speed) {
							vel.y = max_cube_ground_speed;
						}
					} else {
						if (vel.y > max_cube_air_speed) {
							vel.y = max_cube_air_speed;
						}
						if (vel.x > max_cube_air_speed) {
							vel.x = max_cube_air_speed;
						}
					}
					break;
				case 1:
			
					if (onGround || Clinging) {
						if (vel.x > max_sphere_ground_speed) {
							vel.x = max_sphere_ground_speed;
						}
						if (vel.y > max_sphere_ground_speed) {
							vel.y = max_sphere_ground_speed;
						}
					} else {
						if (vel.y > max_sphere_air_speed) {
							vel.y = max_sphere_air_speed;
						}
						if (vel.x > max_sphere_ground_speed) {
							vel.x = max_sphere_ground_speed;
						}
					}

					break;
				case 2:
					if (onGround || Clinging) {
						if (vel.x > max_pyramid_ground_speed) {
							vel.x = max_pyramid_ground_speed;
						}
						if (vel.y > max_pyramid_ground_speed) {
							vel.y = max_pyramid_ground_speed;
						}
					} else {
						if (vel.y > max_pyramid_air_speed) {
							vel.y = max_pyramid_air_speed;
						}
						if (vel.x > max_pyramid_air_speed) {
							vel.x = max_pyramid_air_speed;
						}
					}
					break;
				case 3:
					if (onGround || Clinging) {
						if (vel.x > max_cube_dense_ground_speed) {
							vel.x = max_cube_dense_ground_speed;
						}
						if (vel.y > max_cube_dense_ground_speed) {
							vel.y = max_cube_dense_ground_speed;
						}
					} else {
						if (vel.y > max_cube_dense_air_speed) {
							vel.y = max_cube_dense_air_speed;
						}
						if (vel.x > max_cube_dense_air_speed) {
							vel.x = max_cube_dense_air_speed;
						}
					}
					break;
				}
			}
			rb2d.velocity = vel;
		}
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
			float gravityCounter = 9.8f * rb2d.gravityScale;

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
			float xPrime = -1 * gravityCounter * Mathf.Sin (rot);
			float yPrime = gravityCounter * Mathf.Cos (rot);

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

	void Grounded(GameObject ground)
	{
		if (onGround == false) {
			AudioManager.Singleton.PlaySound ("Landing");
		}
		if (!current_ground.Contains (ground)) {
			current_ground.Add (ground);
		}
		onGround = true;

		CurJumps = MaxJumps;
		WallJump = 0;
		WallDir = -1;
		if (!limit_breaker) {
			parent.Camera.jumpsUpdate (CurJumps + "", MaxJumps);
		} else {
			parent.Camera.jumpsUpdate ("---", MaxJumps);
		}
	}

	void AddWallJumps()
	{
		if (WallJump == 0) {
			WallJump++;
			parent.Camera.jumpsUpdate ((CurJumps + WallJump) + "", MaxJumps);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground") || other.gameObject.CompareTag("Ignore")) {
			Grounded (other.gameObject);
		} else if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag("Player_Bullet")) {
			//MergeParts (other.gameObject);
			parent.Clone.MergeParts (other.gameObject);
		} else if (other.gameObject.CompareTag ("Cling")) {
			if (other.isTrigger) {
				if (parent.ClingCheck (other) && ClingMode == true) {
					Clinging = true;
					clungWall = other.gameObject;
					AudioManager.Singleton.PlaySound ("Landing");
				}
				if (other.gameObject.transform.GetChild (0).CompareTag ("Wall")) {
					if (this.gameObject.transform.position.x > other.gameObject.transform.position.x) {
						if (Clinging) {
							WallDir = 2;
							JumpDir = 2;
						}
					} else {
						if (Clinging) {
							WallDir = 2;
							JumpDir = 4;
						}
					}
					AddWallJumps ();
				} else if (other.gameObject.transform.GetChild (0).CompareTag ("Roof")) {
					if (this.gameObject.transform.position.y > other.gameObject.transform.position.y) {
						Grounded (other.gameObject);
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
			if(current_ground.Remove (other.gameObject)){
				if (current_ground.Count == 0) {
					onGround = false;
				}
			}
		}else if (other.gameObject.CompareTag ("Cling")) {
			if (other.isTrigger) {
				if (other.gameObject == clungWall) {
					Clinging = false;
					WallJump = 0;
					if (!limit_breaker) {
						parent.Camera.jumpsUpdate (CurJumps + "", MaxJumps);
					} else {
						parent.Camera.jumpsUpdate ("---", MaxJumps);
					}
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
