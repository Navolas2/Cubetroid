using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class PlayerUpgrades : MonoBehaviour {

	public static PlayerUpgrades upgrades;
	public Material defaultColor;
	public bool testing = false;
	public bool default_stat = true;

	private Vector3 _PlayerLocation;
	public Vector3 Location {
		get{ return _PlayerLocation; }
		set{ _PlayerLocation = value; }
	}

	private bool _jump;
	public bool jump {
		get { return _jump; }
		set { _jump = value; }
	}

	private int _jumps;
	public int jumps {
		get { return _jumps; }
		set { _jumps = value; }
	}

	private List<Vector3> CollectedJumps;
	public Vector3 CollectedJump{
		set{CollectedJumps.Add (value); }
	}
	public bool AmICollected(Vector3 myLoc){
		if (myLoc == null) {
			return false;
		}
		return false;
		//return CollectedJumps.Contains (myLoc);
	}

	private bool _sphere;
	public bool sphere {
		get { return _sphere; }
		set { _sphere = value; }
	}

	private bool _triangle;
	public bool triangle {
		get { return _triangle; }
		set { _triangle = value; }
	}

	private bool _dense;
	public bool dense {
		get { return _dense; }
		set { _dense = value; }
	}

	private bool _cling;
	public bool cling{
		get { return _cling; }
		set { _cling = value; }
	}

	private List<Color> _mats;
	public List<Color> mats {
		get { return _mats; }
		set { _mats = value; }
	}

	public bool DoesColorExist(Color myCol)
	{
		return _mats.Contains (myCol);
	}

	private bool _split;
	public bool split {
		get { return _split; }
		set{ _split = value; }
	}

	void Awake(){
		if (upgrades == null) {
			DontDestroyOnLoad (gameObject);
			upgrades = this;
			LoadData ();
		} else if (upgrades != this) {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	
	}

	private void DefaultStats()
	{
		if (!testing) {
			_jump = false;
			_jumps = 0;
			_sphere = false;
			_triangle = false;
			_dense = false;
			_cling = false;
			_split = false;
			_mats = new List<Color> (5);
			_mats.Add (defaultColor.color);
			_PlayerLocation = new Vector3 (0, 0, 999);
			CollectedJumps = new List<Vector3> ();
		} else {
			print ("Default");
			_jump = true;
			_jumps = 3;
			_sphere = true;
			_triangle = true;
			_dense = true;
			_cling = true;
			_split = true;
			_mats = new List<Color> (5);
			_mats.Add (defaultColor.color);
			_PlayerLocation = new Vector3 (0, 0, 999);
		}
	}

	public void SaveData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerUpgrades.dat");
		UpgradeData upgrades = new UpgradeData ();
		upgrades.jump = _jump;
		upgrades.jumps = _jumps;
		upgrades.sphere = _sphere;
		upgrades.triangle = _triangle;
		upgrades.dense = _dense;
		upgrades.cling = _cling;
		upgrades.mats = _mats;
		upgrades.split = _split;
		upgrades.Location = _PlayerLocation;

		bf.Serialize (file, upgrades);
		file.Close();
	}

	public void LoadData(){
		if(default_stat){
			DefaultStats ();
		}
		else if (File.Exists (Application.persistentDataPath + "/playerUpgrades.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerUpgrades.dat", FileMode.Open);
			UpgradeData upgrades = (UpgradeData)bf.Deserialize (file);

			_jump = upgrades.jump;
			_jumps = upgrades.jumps;
			_sphere = upgrades.sphere;
			_triangle = upgrades.triangle;
			_dense = upgrades.dense;
			_cling = upgrades.cling;
			_mats = upgrades.mats;
			_split = upgrades.split;
			_PlayerLocation = upgrades.Location;

			file.Close ();
		} else {
			DefaultStats ();
		}
	}
}

[Serializable]
class UpgradeData
{
	private float _PlayerX;
	private float _PlayerY;
	private float _PlayerZ;
	public Vector3 Location {
		get{ return new Vector3(_PlayerX, _PlayerY, _PlayerZ); }
		set{ _PlayerX = value.x;
			_PlayerY = value.y;
			_PlayerZ = value.z;
		}
	}

	private bool _jump;
	public bool jump {
		get { return _jump; }
		set { _jump = value; }
	}

	private int _jumps;
	public int jumps {
		get { return _jumps; }
		set { _jumps = value; }
	}

	private bool _sphere;
	public bool sphere {
		get { return _sphere; }
		set { _sphere = value; }
	}

	private bool _triangle;
	public bool triangle {
		get { return _triangle; }
		set { _triangle = value; }
	}

	private bool _dense;
	public bool dense {
		get { return _dense; }
		set { _dense = value; }
	}

	private bool _cling;
	public bool cling{
		get { return _cling; }
		set { _cling = value; }
	}

	private List<float[]> _mats;
	public List<Color> mats {
		get { List<Color> OutMats = new List<Color> ();
			for(int i = 0; i < _mats.Count; i++){
				OutMats.Add (new Color (_mats [i] [0], _mats [i] [1], _mats [i] [2], _mats [i] [3])); 
					}
			return OutMats; }
		set { _mats = new List<float[]> ();
			for (int i = 0; i < value.Count; i++) {
				_mats.Add (new float[] {value [i].r, value [i].g, value [i].b, value [i].a});
			}
		}
	}

	private bool _split;
	public bool split {
		get { return _split; }
		set{ _split = value; }
	}
}
