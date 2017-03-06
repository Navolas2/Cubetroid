using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class CollectedUpgrades : MonoBehaviour
{
	public static CollectedUpgrades upgrades;

	void Awake(){
		if (upgrades == null) {
			DontDestroyOnLoad (gameObject);
			upgrades = this;
			LoadData ();
		} else if (upgrades != this) {
			Destroy (gameObject);
		}
	}

	private void DefaultStats()
	{
		//Whatever I decide is going to be default
	}

	public void SaveData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/grabbedUpgrades.dat");
		PlayerUpgradeData upgrades = new PlayerUpgradeData ();

		bf.Serialize (file, upgrades);
		file.Close();
	}

	public void LoadData(){
		if (File.Exists (Application.persistentDataPath + "/grabbedUpgrades.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/grabbedUpgrades.dat", FileMode.Open);
			PlayerUpgradeData upgrades = (PlayerUpgradeData)bf.Deserialize (file);


			file.Close ();
		} else {
			DefaultStats ();
		}
	}
}

[Serializable]
class PlayerUpgradeData
{
	//contains whatever this actually needs to contain
}


