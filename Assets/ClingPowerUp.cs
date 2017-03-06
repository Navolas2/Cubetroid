using UnityEngine;
using System.Collections;

public class ClingPowerUp : PowerUp {

	void Start()
	{
		if (PlayerUpgrades.upgrades.cling) {
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		PlayerUpgrades.upgrades.cling = true;
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "cling";
	}
}
