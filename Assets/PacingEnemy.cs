using UnityEngine;
using System.Collections;

public class PacingEnemy : EnemyController {

	private int direction;
	private Rigidbody2D rb2d;
	public int speed = 5;

	private int health;
	private int Unseen;
	private bool InSight;

	// Use this for initialization
	void Start () {
		health = 10;
		direction = 1;
		rb2d = GetComponent<Rigidbody2D> ();
		Unseen = 300;
		InSight = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!InSight && Unseen < 300) {
			Unseen++;
		} else if (!InSight && Unseen >= 300) {
			player = null;
		}
		if (player != null) {
			Vector3 playerPos = player.transform.position;
			Vector3 MyPos = this.transform.position;
			float dir = playerPos.x - MyPos.x;
			if (dir < 0) {
				if (direction != -1) {
					this.transform.Rotate (0, 180, 0, Space.Self);
				}
				direction = -1;

			} else {
				if (direction != 1) {
					this.transform.Rotate (0, 180, 0, Space.Self);
				}
				direction = 1;
			}
		}


		Vector2 movement = new Vector2 (speed * direction, 0);
		rb2d.AddForce (movement);
	}

	public override void Attack (GameObject player_in)
	{
		player = player_in;
		InSight = true;
		Unseen = 0;
	}

	public override void TargetGone (GameObject player_in)
	{
		InSight = false;
	}

	public override void TakeDamage (int damage)
	{
		print ("I'm hit!");
		health -= damage;
		if (health <= 0) {
			//float heals = 4 * Random.value;
			Destroy (this.gameObject);
		}
	}



	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			Attack (other.gameObject);
		}
		else if(!other.isTrigger){
			Vector3 OtherPos = other.transform.position;
			Vector3 MyPos = this.transform.position;

			float dir = OtherPos.x - MyPos.x;
				if (dir > -5 && dir < 5)
				{
					direction *= -1;
					this.transform.Rotate (0, 180, 0, Space.Self);
					rb2d.velocity = new Vector3();
				}
			
		}
	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			TargetGone (other.gameObject);
		}
		try{
		if (other.CompareTag ("Ground")) {

				direction *= -1;
				this.transform.Rotate (0, 180, 0, Space.Self);
				rb2d.velocity = new Vector3();
		}
		}
		catch(UnityException e) {
		}
	}
}
