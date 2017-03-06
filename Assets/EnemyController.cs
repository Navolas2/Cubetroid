using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyController : MonoBehaviour {

	protected Collider2D vision;
	protected GameObject player;

	public abstract void Attack (GameObject player_in);

	public abstract void TargetGone (GameObject player_in);

	public abstract void TakeDamage (int damage);

	/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			Attack (other.gameObject);
		}
	}


	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			TargetGone (other.gameObject);
		}
	}
	*/

}

