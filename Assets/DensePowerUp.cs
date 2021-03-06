﻿using UnityEngine;
using System.Collections;

public class DensePowerUp : PowerUp{

	void Start()
	{
		if (PlayerUpgrades.upgrades.dense) {
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		PlayerUpgrades.upgrades.dense = true;
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "Press LB or 2 to condense down and shrink \nPress RB or 1 to grow to normal";
	}
}
