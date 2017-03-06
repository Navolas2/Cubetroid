using UnityEngine;
using System.Collections;

public class saveController : MonoBehaviour {

	public GameObject camera;
	private CameraController c_control;
	private int startCount;

	// Use this for initialization
	void Start () {
		c_control = camera.GetComponent<CameraController> ();
		startCount = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (startCount != 0) {
			startCount--;
		}
	}

	public void SaveTheGame(){
		print ("YOU SHOULD SAVE!");
		PlayerUpgrades.upgrades.SaveData ();
		c_control.HideSaveOption();
	}

	public void CancelSaving(){
		print ("YOU DON'T WANT TO SAVE");
		c_control.HideSaveOption();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			if (startCount == 0) {
				c_control.DisplaySaveOption ();
			}
		}
	}


	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player")){
			c_control.HideSaveOption();
		}
	}
}
