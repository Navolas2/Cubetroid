using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloneManager : MonoBehaviour {
	
	private int SplitCount;
	public Rigidbody2D Player_Rigid;
	public Rigidbody2D Bullet_Rigid;
	private Rigidbody2D rb2d;
	private List<Rigidbody2D> clones;
	public bool IsMain;
	private TargetReticle target;
	private PlayerControllerVersion2 parent;
	// Use this for initialization
	public void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		if (clones == null) {
			clones = new List<Rigidbody2D> ();
			clones.Add (rb2d);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(SplitCount > 0){SplitCount--;}
		//clones character
		if (Input.GetKeyDown (KeyCode.U) && IsMain && PlayerUpgrades.upgrades.split) {
			SplitCount = 10;
			Vector2 shift = target.GetPosition ();
			float splitSpeed = 10f;
			Rigidbody2D testClone = (Rigidbody2D)Instantiate (Player_Rigid, new Vector3(gameObject.transform.position.x + shift.x, gameObject.transform.position.y + shift.y), gameObject.transform.rotation);
			testClone.velocity = new Vector2 (splitSpeed * shift.x, splitSpeed * shift.y);
			testClone.GetComponent<CloneManager> ().IsMain = false;
			clones.Add (testClone);
			testClone.GetComponent<CloneManager> ().CloneList = clones;
			parent.SetUpClone (testClone);

		}
		//clone and shoot bullet
		if (Input.GetKeyDown (KeyCode.O) && IsMain && PlayerUpgrades.upgrades.split) {
			SplitCount = 10;
			Vector2 shift = target.GetPosition ();
			float rot = ForceManager.FindAngleToCenter (target.gameObject, this.gameObject);
			rot = Mathf.Rad2Deg * rot + 90;
			float splitSpeed = 15f;
			Rigidbody2D testClone = (Rigidbody2D)Instantiate (Bullet_Rigid, new Vector3(gameObject.transform.position.x + shift.x, gameObject.transform.position.y + shift.y), gameObject.transform.rotation);
			testClone.transform.Rotate (0, 0, rot, Space.Self);
			testClone.velocity = new Vector2 (splitSpeed * shift.x, splitSpeed * shift.y);

			//testClone.GetComponent<PlayerControllerVersion2> ().IsMain = false;
			//testClone.GetComponent<PlayerControllerVersion2> ().Start();


			testClone.GetComponent<CloneManager> ().CloneList = clones;
			parent.HealthBar.ReduceOne ();
			//testClone.GetComponent<PlayerControllerVersion2> ().GetHealthBar().SetHealth (healthManager.ReduceOne());
			clones.Add (testClone);
		}
	}

	public void MergeParts(GameObject partB){
		if (SplitCount == 0) {
			CloneManager clone = partB.GetComponent<CloneManager> ();
			if (GetIndex () < clone.GetIndex () && (GetIndex() != -1) && (clone.GetIndex() != -1)) {
				Vector2 hp;
				if (clone.CompareTag ("Player_Bullet")) {
					hp = new Vector2 (1, 1);
				} else
				{
					hp = clone.GetHealth ();
				}
				parent.HealthBar.MergeHealth (hp);
				clones.Remove (clone.RigidBody);
				Destroy (clone.gameObject);
			}
		}
	}

	/************************************/
	//GETTERS AND SETTERS
	/************************************/

	public int GetIndex(){
		return clones.IndexOf(rb2d);
	}

	public List<Rigidbody2D> CloneList
	{
		get { return clones; }
		set { clones = value; }
	}

	public Rigidbody2D RigidBody
	{
		get{return rb2d;}
		set{ rb2d = value; }
	}
		
	public TargetReticle Target{
		set{ target = value; }
	}

	public Vector2 GetHealth()
	{
		if (this.CompareTag ("Player")) {
			return parent.HealthBar._Health;
		} else {
			return new Vector2 (1, 1);
		}
	}

	public PlayerControllerVersion2 Parent
	{
		set{parent = value;}
	}


}
