using UnityEngine;
using System.Collections;

public class saveController : MonoBehaviour {

	public GameObject camera;
	private CameraController c_control;
	private int startCount;
	public Room_Vision save_vision;

	public bool offer_save = true;
	public bool premenant_effect = true;
	// Use this for initialization
	void Start () {
		if (camera != null) {
			c_control = camera.GetComponent<CameraController> ();
		}
		startCount = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (startCount != 0) {
			startCount--;
		}
	}

	public void SaveTheGame(){
		//print ("YOU SHOULD SAVE!");
		PlayerUpgrades.upgrades.SaveData ();
		c_control.HideSaveOption();
	}

	public void CancelSaving(){
		//print ("YOU DON'T WANT TO SAVE");
		c_control.HideSaveOption();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			if (startCount == 0 && offer_save) {
				c_control.DisplaySaveOption ();
			}
			if (save_vision != null) {
				save_vision.Make_Invisible ();
				save_vision.Put_in_Room ();
			}
		}
	}
	/*
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			if (offer_save) {
				c_control.TryShowSaveOption ();
			}
		}
	}
	*/
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player")){
			if (offer_save) {
				//c_control.HideAllOptions();
			}
			if (save_vision != null) {
				if (!premenant_effect) {
					save_vision.Make_Visible ();
				}
			}
		}
	}
}
