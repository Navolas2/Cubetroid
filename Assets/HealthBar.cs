using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	 
	private float CurHealth = -1;
	private float MaxHealth = -1;
	public int iFrames = 30;
	private int curiFrame;
	private Vector3 defaultScale;
	private int formIndex;
	private Vector3[] formScale;
	private TextMesh healthNumber;

	private Material[] MatArray;
	public Material mat1;
	public Material mat2;
	// Use this for initialization
	void Start () {
		formIndex = 0;
		healthNumber = GetComponentInChildren<TextMesh> ();
		if (CurHealth == -1 && MaxHealth == -1) {
			CurHealth = 100;
			MaxHealth = 100;
		}
		healthNumber.text = CurHealth + "";
		curiFrame = iFrames;
		defaultScale = gameObject.transform.localScale;
		LoadMats ();
		LoadScales ();
		ChangeSize ();
	}

	void LoadMats()
	{
		MatArray = new Material[2];
		MatArray [0] = mat1;
		MatArray [1] = mat2;
	}

	void LoadScales()
	{
		formScale = new Vector3[4];
		formScale [0] = defaultScale;
		formScale [1] = defaultScale;
		formScale [2] = new Vector3 (0.3650083f, 0.3650083f, 0.3650083f);
		formScale [3] = new Vector3 (0.1747331f, 0.1747331f, 0.1747331f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (IsInvincible ()) {
			curiFrame++;
			TransparencyAdjust ();
		}
		/*
		Vector3 position = new Vector3 ();
		position.z = -100.5f;
		Vector3 scale = new Vector3 ();
		Vector3 curScale = CurrentScale ();
		if (Input.GetButtonDown("cube")) {
			formIndex = 0;
			//gameObject.transform.position = position;

			gameObject.transform.localPosition = position;
			gameObject.transform.localScale = MultiplyVector(defaultScale, CurrentScale());
		}
		else if (Input.GetButtonDown("sphere") && PlayerUpgrades.upgrades.sphere) {
			formIndex = 1;
			//gameObject.transform.position = position;
			gameObject.transform.localPosition = position;
			gameObject.transform.localScale = MultiplyVector(defaultScale, CurrentScale());
		}
		else if (Input.GetKeyDown (KeyCode.Alpha6) && PlayerUpgrades.upgrades.triangle) {
			formIndex = 2;
			scale = MultiplyVector (formScale [formIndex], curScale);
			position.y = -13.73f;
			gameObject.transform.localPosition = position;
			//gameObject.transform.position = position;
			gameObject.transform.localScale = scale;

		}
		else if (Input.GetButtonDown("dense") && PlayerUpgrades.upgrades.dense) {
			formIndex = 3;
			position.z = -20.75f;
			scale = MultiplyVector (formScale [formIndex], curScale);
			gameObject.transform.localPosition = position;
			gameObject.transform.localScale = scale;
		}*/
	}

	public void ChangeCube(){
		Vector3 position = new Vector3 ();
		position.z = -100.5f;
		Vector3 scale = new Vector3 ();
		Vector3 curScale = CurrentScale ();

		formIndex = 0;
		//gameObject.transform.position = position;

		gameObject.transform.localPosition = position;
		gameObject.transform.localScale = MultiplyVector(defaultScale, CurrentScale());
	}

	public void ChangeDense(){
		Vector3 position = new Vector3 ();
		position.z = -100.5f;
		Vector3 scale = new Vector3 ();
		Vector3 curScale = CurrentScale ();

		formIndex = 3;
		position.z = -20.75f;
		scale = MultiplyVector (formScale [formIndex], curScale);
		gameObject.transform.localPosition = position;
		gameObject.transform.localScale = scale;
	}

	public void ChangeSphere(){
		Vector3 position = new Vector3 ();
		position.z = -100.5f;
		Vector3 scale = new Vector3 ();
		Vector3 curScale = CurrentScale ();

		formIndex = 1;
		//gameObject.transform.position = position;
		gameObject.transform.localPosition = position;
		gameObject.transform.localScale = MultiplyVector(defaultScale, CurrentScale());
	}

	Vector3 MultiplyVector(Vector3 v_1, Vector3 v_2)
	{
		Vector3 out_Vec = new Vector3();
		out_Vec.x = v_1.x * v_2.x;
		out_Vec.y = v_1.y * v_2.y;
		out_Vec.z = v_1.z * v_2.z;
		return out_Vec;
	}

	public void AdjustHealth(int damage)
	{
		if (damage > 0) {
			curiFrame = 0;
		}
		CurHealth -= damage;
		if (CurHealth > MaxHealth) {
			CurHealth = MaxHealth;
		}
		ChangeSize ();
		if (IsDead ()) {
			AudioManager.Singleton.PlaySound ("KO");
			PlayerUpgrades.upgrades.Game_Over ();
		}
	}
		
	void ChangeSize()
	{
		healthNumber.text = CurHealth + "";
		Transform t = this.GetComponent<Transform> ();
		Vector3 curScale = formScale [formIndex];
		float scale = curScale.x * (CurHealth / MaxHealth);
		Vector3 N_Scale = new Vector3 (scale, scale, scale);
		t.localScale = N_Scale;
	}

	Vector3 CurrentScale()
	{
		float scale = (CurHealth / MaxHealth);
		return new Vector3 (scale, scale, scale);
	}

	public void TransparencyAdjust ()
	{
		for (int i = 0; i < MatArray.Length; i++) {
			if (curiFrame % 5 == 0 || curiFrame % 5 == 1) {
				Color N_Color = MatArray [i].color;
				N_Color.a = 1f;
				MatArray [i].color = N_Color;
			} else {
				Color N_Color = MatArray [i].color;
				N_Color.a = 0.1f;
				MatArray [i].color = N_Color;
			}
		}
	}
	public Vector2 HalfHealth()
	{
		float h_health = CurHealth / 2;
		CurHealth = CurHealth / 2;
		ChangeSize ();
		return new Vector2 (h_health, MaxHealth);
	}

	public void MergeHealth(Vector2 hp)
	{
		CurHealth += hp.x;
		if (CurHealth > MaxHealth) {
			CurHealth = MaxHealth;
		}
		ChangeSize ();
	}

	public void ReduceOne ()
	{
		CurHealth--;
		ChangeSize();
	}

	/************************************/
	//GETTERS AND SETTERS
	/************************************/

	public Vector2 _Health
	{
		get{ return new Vector2 (CurHealth, MaxHealth); }
		set {
			CurHealth = value.x;
			MaxHealth = value.y;
		}
	}

	public bool IsInvincible()
	{
		return (curiFrame < iFrames);
	}

	public bool IsDead()
	{
		return CurHealth <= 0;
	}

}
