using UnityEngine;
using System.Collections;

public class SpherePowerUp : PowerUp {

	// Use this for initialization
	void Start()
	{
		if (PlayerUpgrades.upgrades.sphere) {
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		PlayerUpgrades.upgrades.sphere = true;
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "sphere";
	}
}
