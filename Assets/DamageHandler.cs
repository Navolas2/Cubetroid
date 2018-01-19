using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {

	public int Damage;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player") && this.CompareTag("Hazard")) {
			PlayerControllerVersion2 p = other.gameObject.GetComponent<PlayerControllerVersion2> ();
			p.TakeDamage (Damage);
			AudioManager.Singleton.PlaySound ("Hit_Player");
		}
		if (other.CompareTag ("Player_Bullet") && this.CompareTag ("Enemy")) {
			EnemyController E_in = this.gameObject.GetComponent<EnemyController> ();
			E_in.TakeDamage (1);
			AudioManager.Singleton.PlaySound ("Hit_Enemy");
		}
		if (other.CompareTag ("Enemy") && this.CompareTag ("Player")) {
			if (other.bounds.max.y < this.GetComponent<Collider2D>().bounds.min.y){// && other.transform.position.y > other.bounds.min.y) {
				if (other.bounds.max.x > this.transform.position.x && this.transform.position.x > other.bounds.min.x) {
					if (!other.isTrigger) {
						//Damage Enemy
						EnemyController E_in = other.gameObject.GetComponent<EnemyController> ();
						E_in.TakeDamage (Damage);
						AudioManager.Singleton.PlaySound ("Hit_Enemy");
					}
				}
			}
		}
		if (other.CompareTag ("Player") && this.CompareTag ("Enemy")) {
			//print ("I hit player?");
			if (!(other.bounds.max.y < this.GetComponent<Collider2D>().bounds.min.y)){//  && !( other.transform.position.y > other.bounds.min.y)) {
				//print (other.bounds.max.x);
				//print (other.bounds.min.x);
				//print (this.GetComponent<Collider2D> ().bounds.max.x);
				//print (this.GetComponent<Collider2D> ().bounds.min.x);
				if (this.GetComponent<Collider2D> ().bounds.min.y > other.bounds.max.y) {
					//Damage Player because Below
					PlayerControllerVersion2 p = other.gameObject.GetComponent<PlayerControllerVersion2> ();
					p.TakeDamage (Damage);
					AudioManager.Singleton.PlaySound ("Hit_Player");
				} else {
					if (other.bounds.max.x < this.GetComponent<Collider2D> ().bounds.max.x) {
						float dist = this.GetComponent<Collider2D> ().bounds.max.x - other.bounds.max.x;
						if (dist < .7) {
							//damage Player because against
							PlayerControllerVersion2 p = other.gameObject.GetComponent<PlayerControllerVersion2> ();
							p.TakeDamage (Damage);
							AudioManager.Singleton.PlaySound ("Hit_Player");
						}
					} 
					if (other.bounds.min.x > this.GetComponent<Collider2D> ().bounds.min.x) {
						float dist = other.bounds.min.x - this.GetComponent<Collider2D> ().bounds.min.x;
						if (dist < .7) {
							//damage Player because against
							PlayerControllerVersion2 p = other.gameObject.GetComponent<PlayerControllerVersion2> ();
							p.TakeDamage (Damage);
							AudioManager.Singleton.PlaySound ("Hit_Player");
						}
					}
				}
				//|| this.transform.position.x > other.bounds.min.x) {
					//Damage Enemy
				}
			}
	}

	bool ComparePosition(float OtherY, float MyY)
	{
		//return(
		return false;
	}
}
