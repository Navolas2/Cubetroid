using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;

	private Vector3 offset;

	private UnityEngine.UI.Text health;
	private UnityEngine.UI.Text jump;
	private UnityEngine.UI.Text saveText;
	private UnityEngine.UI.Button yes_button_save;
	private UnityEngine.UI.Button no_button_save;
	private UnityEngine.UI.Button yes_button_exit;
	private UnityEngine.UI.Button no_button_exit;
	private bool started = false;
	public bool Active = false;


	// Use this for initialization
	public void Start () {
		if (!started) {
			offset = transform.position - target.transform.position;
			//print (GetComponentsInChildren<UnityEngine.UI.Text> ().Length);
			health = GetComponentsInChildren<UnityEngine.UI.Text> () [1];
			jump = GetComponentsInChildren<UnityEngine.UI.Text> () [3];
			saveText = GetComponentsInChildren<UnityEngine.UI.Text> () [4];
			yes_button_save = GetComponentsInChildren<UnityEngine.UI.Button> () [0];
			no_button_save = GetComponentsInChildren<UnityEngine.UI.Button> () [1];
			yes_button_exit = GetComponentsInChildren<UnityEngine.UI.Button> () [2];
			no_button_exit = GetComponentsInChildren<UnityEngine.UI.Button> () [3];
			HideAllOptions ();
			saveController s_con = FindObjectOfType<saveController> ();
			PlayerUpgrades p_up = FindObjectOfType<PlayerUpgrades> ();
			UnityEngine.UI.Button.ButtonClickedEvent b_event_1 = new UnityEngine.UI.Button.ButtonClickedEvent ();
			b_event_1.AddListener (s_con.SaveTheGame);
			yes_button_save.onClick = b_event_1;

			UnityEngine.UI.Button.ButtonClickedEvent b_event_2 = new UnityEngine.UI.Button.ButtonClickedEvent ();
			b_event_2.AddListener (s_con.CancelSaving);
			no_button_save.onClick = b_event_2;

			UnityEngine.UI.Button.ButtonClickedEvent b_event_3 = new UnityEngine.UI.Button.ButtonClickedEvent ();
			b_event_3.AddListener (PlayerUpgrades.upgrades.Title_Screen);
			yes_button_exit.onClick = b_event_3;

			UnityEngine.UI.Button.ButtonClickedEvent b_event_4 = new UnityEngine.UI.Button.ButtonClickedEvent ();
			b_event_4.AddListener (PlayerUpgrades.upgrades.continue_game);
			no_button_exit.onClick = b_event_4;

			started = true;
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetButtonDown ("Submit")) {
			HideAllOptions ();
		}
		transform.position = target.transform.position + offset;
		if (Active) {
			if (Input.GetButtonDown ("cube")) {
				if (yes_button_save.IsActive()) {
					yes_button_save.onClick.Invoke ();
				} else {
					yes_button_exit.onClick.Invoke ();
				}
			} else if (Input.GetButtonDown ("dense")) {
				if (yes_button_save.IsActive()) {
					no_button_save.onClick.Invoke ();
				} else {
					no_button_exit.onClick.Invoke ();
				}
			}
		}
	}

	public void healthUpdate(Vector2 hp)
	{
		health.text = hp.x + " / " + hp.y;
	}

	public void jumpsUpdate(string curJ, int maxJ)
	{
		jump.text = curJ + " / " + maxJ;
	}

	public void DisplayPowerUp(string info){
		saveText.enabled = true;
		info = info + "\nPress Start or Enter to exit";
		saveText.text = info;
	}

	public void TryShowSaveOption(){
		if (!Active) {
			DisplaySaveOption ();
			Active = true;
		}
	}

	public void DisplaySaveOption(){
		Active = true;
		saveText.enabled = true;
		saveText.text = "Would you like to save?";
		yes_button_save.gameObject.SetActive (true);
		no_button_save.gameObject.SetActive (true);
	}

	public void HideSaveOption(){
		//saveText.enabled = false;
		yes_button_save.gameObject.SetActive (false);
		no_button_save.gameObject.SetActive (false);
		DisplayQuitOptions ();
	}

	public void DisplayQuitOptions(){
		Active = true;
		saveText.text = "Would you like to quit?";
		yes_button_exit.gameObject.SetActive (true);
		no_button_exit.gameObject.SetActive (true);
	}

	public void HideOptions(){
		saveText.enabled = false;
		yes_button_exit.gameObject.SetActive (false);
		no_button_exit.gameObject.SetActive (false);
		Active = false;
	}

	public void HideAllOptions(){
		saveText.enabled = false;
		Active = false;
		yes_button_save.gameObject.SetActive (false);
		no_button_save.gameObject.SetActive (false);
		yes_button_exit.gameObject.SetActive (false);
		no_button_exit.gameObject.SetActive (false);
	}
}
