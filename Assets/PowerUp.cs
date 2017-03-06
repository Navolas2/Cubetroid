using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public abstract void Enable ();

	public abstract string getPowerUp ();
}
