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
		return "press X button or 3 to transform into a Sphere\n Click L-stick or press Z for umlimited speed";
	}
}
