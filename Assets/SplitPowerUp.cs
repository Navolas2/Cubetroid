using UnityEngine;
using System.Collections;

public class SplitPowerUp : PowerUp {

	void Start()
	{
		if (PlayerUpgrades.upgrades.split) {
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		PlayerUpgrades.upgrades.split = true;
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "split";
	}
}
