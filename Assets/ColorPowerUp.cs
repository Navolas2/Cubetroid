using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorPowerUp : PowerUp {

	public Material myColor;

	void Start()
	{
		if (PlayerUpgrades.upgrades.DoesColorExist(myColor.color)) {
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		PlayerUpgrades.upgrades.mats.Add(myColor.color);
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "Gives access to a new color\nChange colors by clicking the right stick\nOr by pressing Q or E";
	}
}
