using UnityEngine;
using System.Collections;

public class PrisimPowerUp : PowerUp {

	void Start()
	{
		if (PlayerUpgrades.upgrades.triangle) {
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		PlayerUpgrades.upgrades.triangle = true;
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "prism";
	}
}
