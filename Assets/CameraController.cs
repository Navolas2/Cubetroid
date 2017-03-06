using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;

	private Vector3 offset;

	private UnityEngine.UI.Text health;
	private UnityEngine.UI.Text jump;
	private UnityEngine.UI.Text saveText;
	private UnityEngine.UI.Button yes_button;
	private UnityEngine.UI.Button no_button;
	private bool started = false;


	// Use this for initialization
	public void Start () {
		if (!started) {
			print ("START");
			offset = transform.position - target.transform.position;
			//print (GetComponentsInChildren<UnityEngine.UI.Text> ().Length);
			health = GetComponentsInChildren<UnityEngine.UI.Text> () [1];
			jump = GetComponentsInChildren<UnityEngine.UI.Text> () [3];
			saveText = GetComponentsInChildren<UnityEngine.UI.Text> () [4];
			yes_button = GetComponentsInChildren<UnityEngine.UI.Button> () [0];
			no_button = GetComponentsInChildren<UnityEngine.UI.Button> () [1];
			HideSaveOption ();
			started = true;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.transform.position + offset;
	}

	public void healthUpdate(Vector2 hp)
	{
		health.text = hp.x + " / " + hp.y;
	}

	public void jumpsUpdate(int curJ, int maxJ)
	{
		jump.text = curJ + " / " + maxJ;
	}

	public void DisplaySaveOption(){
		saveText.enabled = true;
		yes_button.gameObject.SetActive (true);
		no_button.gameObject.SetActive (true);
	}

	public void HideSaveOption(){
		saveText.enabled = false;
		yes_button.gameObject.SetActive (false);
		no_button.gameObject.SetActive (false);
	}
}
