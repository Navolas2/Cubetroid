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
		return "Press Y or C to activate cling mode\nStick to surfaces the same color as you\nLeft and Right to move up and down on wall";
	}
}
