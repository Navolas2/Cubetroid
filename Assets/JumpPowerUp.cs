using UnityEngine;
using System.Collections;

public class JumpPowerUp : PowerUp {

	private Vector3 myLocation;
	void Start()
	{
		myLocation = this.gameObject.transform.position;
		if(PlayerUpgrades.upgrades.AmICollected(myLocation)){
			Destroy (this.gameObject);
		}
	}

	override public void Enable ()
	{
		//Display Powerup Information
		if (PlayerUpgrades.upgrades.jump) {
			PlayerUpgrades.upgrades.jumps = PlayerUpgrades.upgrades.jumps + 1;
		} else {
			PlayerUpgrades.upgrades.jump = true;
			PlayerUpgrades.upgrades.jumps = 1;
		}
		PlayerUpgrades.upgrades.CollectedJump = myLocation;
		Destroy (this.gameObject);
	}

	override public string getPowerUp()
	{
		return "jump";
	}
}
