using UnityEngine;
using System.Collections;

public class PacingEnemy : EnemyController {

	private int direction;
	private Rigidbody2D rb2d;
	public int speed = 5;
	public int max_speed = 10;

	public int lost_target_time = 300;
	public int flip_check_time = 40;

	private int health;
	private int Unseen;
	private bool InSight;

	public GameObject standing_ground;
	Vector3 last_position;
	int tick_counter = 0;
	// Use this for initialization
	void Start () {
		health = 10;
		direction = 1;
		rb2d = GetComponent<Rigidbody2D> ();
		Unseen = 300;
		InSight = false;
		last_position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!InSight && Unseen < lost_target_time) {
			Unseen++;
		} else if (!InSight && Unseen >= lost_target_time) {
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
		tick_counter++;
		tick_counter %= flip_check_time;
		if (tick_counter == 0) {
			float last = last_position.magnitude;
			float curr = transform.position.magnitude;
			float diff = Mathf.Abs (last - curr);
			if (diff < 2) {
				direction *= -1;
				this.transform.Rotate (0, 180, 0, Space.Self);
			}
			last_position = transform.position;
		}


		Vector2 movement = new Vector2 (speed * direction, 0);
		rb2d.AddForce (movement);
		Vector2 vel = rb2d.velocity;
		if (vel.x > max_speed) {
			vel.x = max_speed;
		} else if (vel.x < (max_speed * -1)) {
			vel.x = (max_speed * -1);
		}
		rb2d.velocity = vel;
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
		}else if (other.CompareTag ("Ground")) {
			standing_ground = other.gameObject;
		}
		else if(!other.isTrigger && !other.CompareTag("Ignore")){
			Vector3 OtherPos = other.transform.position;
			Vector3 MyPos = this.transform.position;

			float dir = OtherPos.x - MyPos.x;
			if (dir > -5 && dir < 5)
			{
				direction *= -1;
				this.transform.Rotate (0, 180, 0, Space.Self);
				rb2d.velocity = new Vector3();
				last_position = transform.position;
				tick_counter = 0;
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
			if(other.gameObject == standing_ground){
				direction *= -1;
				this.transform.Rotate (0, 180, 0, Space.Self);
				rb2d.velocity = new Vector3();
				last_position = transform.position;
				tick_counter = 0;
				}
			}
		}
		catch(UnityException e) {
		}
	}
}
