using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerVersion2 : MonoBehaviour {

	//private Vector3 defaultScale;
	private HealthBar healthManager;

	//private Rigidbody2D rb2d;
	private int formIndex;

	private GameObject curForm;
	public CameraController cam;

	private Animator anim;

	private CollisionManager colManager;
	private MaterialManager matManager;
	private ForceManager force_man;
	private CloneManager clone_Manager;
	private TargetReticle target;

	public bool IsMain;

	public void Start()
	{
		//SplitCount = 0;
		matManager = GetComponent<MaterialManager> ();
		force_man = GetComponent<ForceManager> ();
		force_man._Parent = this;
		//rb2d = GetComponent<Rigidbody2D> ();

		anim = GetComponent<Animator> ();
		//WallDir = -1;

		anim.SetInteger ("formIndex", formIndex);

		matManager.RenderList = GetComponentsInChildren<Renderer> ();
		matManager.StartManager (force_man._ClingMode);
		colManager = GetComponent<CollisionManager> ();
	
		colManager.ColliderControl (formIndex);

		//defaultScale = this.gameObject.transform.localScale;
		healthManager = GetComponentInChildren<HealthBar> ();
		target = GetComponentInChildren<TargetReticle> ();
		clone_Manager = GetComponent<CloneManager> ();
		clone_Manager.Start ();
		clone_Manager.Target = target;

		clone_Manager.Parent = this;

		//SetStats ();

		if (IsMain) {
			cam.Start ();
			cam.healthUpdate (healthManager._Health);
			Vector2 jumps = force_man._Jumps;
			cam.jumpsUpdate (jumps.x + "", (int)jumps.y);
			if (PlayerUpgrades.upgrades.Location.z != 999) {
				this.transform.position = PlayerUpgrades.upgrades.Location;
			}
		} else {
			Destroy (target.gameObject);
		}
			
	}

	void FixedUpdate()
	{
		if (!cam.Active) {
			if (Input.GetButtonDown ("cube")) {
				changeForm (0);
				healthManager.ChangeCube ();
			} else if (Input.GetButtonDown ("sphere") && PlayerUpgrades.upgrades.sphere) {
				changeForm (1);
				HealthBar.ChangeSphere ();
			} else if (Input.GetKeyDown (KeyCode.Alpha6) && PlayerUpgrades.upgrades.triangle) {
				changeForm (2);
			} else if (Input.GetButtonDown ("dense") && PlayerUpgrades.upgrades.dense) {
				changeForm (3);
				healthManager.ChangeDense ();
			}

			if (Input.GetButtonDown ("Cling") && PlayerUpgrades.upgrades.cling) {
				AudioManager.Singleton.PlaySound ("Cling");
				force_man._ClingMode = !force_man._ClingMode;
				matManager.GlowTexture (force_man._ClingMode);
			}
		}
	}

	void changeForm(int nForm)
	{
		if (nForm != formIndex) {
			AudioManager.Singleton.PlaySound ("Morph");
		}
		if (nForm != formIndex) {
			formIndex = nForm;
			anim.SetInteger ("formIndex", nForm);
			if (force_man.IsOnGround()) {
				Vector3 rotVec = gameObject.transform.rotation.eulerAngles;
				rotVec.z += rotVec.z * -1;
				gameObject.transform.Rotate (rotVec, Space.Self);
			} else if (!force_man.IsOnGround() && nForm == 2) {
				Vector3 rotVec = gameObject.transform.rotation.eulerAngles;
				rotVec.z += rotVec.z * -1 + 180;
				gameObject.transform.Rotate (rotVec, Space.Self);
			}
			colManager.ColliderControl(formIndex);
		}
	}


	public void TakeDamage(int damage)
	{
		//check iFrames

		if(!healthManager.IsInvincible())
		{
			//Damage Reduction
			healthManager.AdjustHealth(damage);

			//Damage Reduction
			//check if dead
			if (healthManager.IsDead()) {
				print ("DEAD");
				//game over
			}
			cam.healthUpdate (healthManager._Health);
		}
	}

	public bool ClingCheck(Collider2D other)
	{
		return other.gameObject.GetComponent<Renderer> ().material.color.Equals (matManager.GetRenderAtIndex(0).material.color);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Power_Up")) {
			AudioManager.Singleton.PlaySound ("PowerUp");

			PowerUp p = other.GetComponent<PowerUp> ();
			cam.DisplayPowerUp (p.getPowerUp ());
			p.Enable ();
			if(p.getPowerUp().CompareTo("jump") == 0){
				force_man.UpdateJumps ();
			}
		}
		if (other.CompareTag ("Save_Station")) {
			healthManager.AdjustHealth (-1000);
			cam.healthUpdate (healthManager._Health);
			PlayerUpgrades.upgrades.Location = this.transform.position;
		}
	}

	/*
	void OnTriggerExit2D(Collider2D other)
	{
		
	}*/

	public void SetUpClone (Rigidbody2D testClone){
		testClone.GetComponent<PlayerControllerVersion2> ().IsMain = false;
		testClone.GetComponent<PlayerControllerVersion2> ().Start();
		testClone.GetComponent<PlayerControllerVersion2> ().Form = formIndex;
		testClone.GetComponent<PlayerControllerVersion2> ().ClingMode = ClingMode;

		testClone.GetComponent<PlayerControllerVersion2> ().HealthBar._Health = healthManager.HalfHealth();
		testClone.GetComponent<MaterialManager> ().MaterialIndex = matManager.MaterialIndex;

	}

	/************************************/
	//GETTERS AND SETTERS
	/************************************/

	public bool ClingMode
	{
		get {return force_man._ClingMode;}
		set {
			force_man._ClingMode = value;
			matManager.GlowTexture (value);
		}
	}

	public bool Clinging
	{
		get {return force_man._Clinging;}
	}

	public int Form
	{
		get{ return formIndex; }
		set{changeForm (value);}
	}

	public HealthBar HealthBar
	{
		get{ return healthManager; }
	}

/*	public Rigidbody2D RigidBody
	{
		get{return rb2d;}
	}
	*/

	public CameraController Camera
	{
		get{return cam;}
	}

	public CloneManager Clone
	{
		get{return clone_Manager;}
	}
}
