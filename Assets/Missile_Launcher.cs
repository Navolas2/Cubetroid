using UnityEngine;
using System.Collections;

public class Missile_Launcher : EnemyController {

	//private Rigidbody2D rb2d;
	public Rigidbody2D missile;
	private bool isDestroyed;
	public float launchSpeed = 15f;
	public bool play_sound = true;

	private int health;

	// Use this for initialization
	void Start () {
		health = 10;
		isDestroyed = false;
		//rb2d = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {
		

	}

	public override void Attack (GameObject player_in)
	{
		if(!isDestroyed){
		float rot = -20;
		//rot = Mathf.Rad2Deg * rot + 90;
			if (play_sound) {
				AudioManager.Singleton.PlaySound ("Missile_Launch");
			}
		Rigidbody2D testClone = (Rigidbody2D)Instantiate (missile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
		testClone.transform.Rotate (0, 0, rot, Space.Self);
		testClone.GetComponent<Missile_Controller> ().Start ();
		testClone.GetComponent<Missile_Controller> ().Launch (launchSpeed, player_in, rot * -1);
		//testClone.velocity = new Vector2 (splitSpeed * shift.x, splitSpeed * shift.y);
		}
	}

	public override void TargetGone (GameObject player_in)
	{
		
	}

	public override void TakeDamage (int damage)
	{
		print ("I'm hit!");
		health -= damage;
		if (health <= 0) {
			this.isDestroyed = true;
			//Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
	}


	void OnTriggerExit2D(Collider2D other)
	{
		
	}
}
