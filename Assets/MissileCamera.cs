using UnityEngine;
using System.Collections;

public class MissileCamera : MonoBehaviour {

	public Missile_Launcher m_launcher;
	private int sight_count = 0;
	public int fire_rate = 40;

	void FixedUpdate(){
		if (sight_count > 0) {
			sight_count--;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			m_launcher.Attack (other.gameObject);
			sight_count = fire_rate;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (sight_count == 0) {
			if (other.CompareTag ("Player")) {
				m_launcher.Attack (other.gameObject);
				sight_count = fire_rate;
			}
		}
	}
}
