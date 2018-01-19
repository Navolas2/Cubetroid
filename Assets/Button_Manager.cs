using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour {

	public List<string> button_names;
	private UnityEngine.UI.Button Left_Button;
	private UnityEngine.UI.Button Right_Button;
	// Use this for initialization
	void Start () {
		UnityEngine.UI.Button [] buttons = GetComponentsInChildren<UnityEngine.UI.Button>();
		Left_Button = buttons [1];
		Right_Button = buttons [0];
		for (int i = 0; i < buttons.Length; i++) {
			UnityEngine.UI.Button.ButtonClickedEvent b_event = new UnityEngine.UI.Button.ButtonClickedEvent ();
			if (button_names.Count > i) {
				switch (button_names [i]) {
				case "Continue":
					b_event.AddListener (PlayerUpgrades.upgrades.continue_game);
					buttons [i].onClick = b_event;
					break;
				case "New Game":
					b_event.AddListener (PlayerUpgrades.upgrades.new_game);
					buttons [i].onClick = b_event;
					break;
				case "Title":
					b_event.AddListener (PlayerUpgrades.upgrades.Title_Screen);
					buttons [i].onClick = b_event;
					break;
				
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("cube")) {
			Right_Button.onClick.Invoke ();
		} else if (Input.GetButtonDown ("dense")) {
			Left_Button.onClick.Invoke ();
		}
	}
}
