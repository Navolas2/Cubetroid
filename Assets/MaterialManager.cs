using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialManager : MonoBehaviour {

	private PlayerControllerVersion2 player;

	private Material[] MatArray;
	private int MatIndex;
	public Material mat1;
	public Material mat2;
	public Material mat3;
	public Material mat4;
	public Material mat5;

	private Renderer[] renderList;

	// Use this for initialization
	public void StartManager (bool CurCling) {
		player = GetComponent<PlayerControllerVersion2> ();
		LoadMats (CurCling);
	}

	void LoadMats(bool CurCling)
	{
		MatArray = new Material[5];
		MatArray [0] = mat1;
		MatArray [1] = mat2;
		MatArray [2] = mat3;
		MatArray [3] = mat4;
		MatArray [4] = mat5;
		GlowTexture (CurCling);
	}

	public void GlowTexture(bool status)
	{
		ChangeMaterial ();
		for (int i = 0; i < MatArray.Length; i++) {
			if (status) {
				MatArray [i].SetColor ("_EmissionColor", MatArray [i].color);
			} else {
				MatArray [i].SetColor ("_EmissionColor", new Color());
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if (Input.GetButtonDown("Scroll_Left")) {
			Debug.Log ("pressed Q");
			if (!player.Clinging) {
				if (MatIndex == 0) {
					MatIndex = 4;
				} else {
					MatIndex--;
				}
				CheckMaterial (-1);
				ChangeMaterial ();
			}
		}
		if (Input.GetButtonDown("Scroll_Right")) {
			if (!player.Clinging) {
				if (MatIndex == 4) {
					MatIndex = 0;
				} else {
					MatIndex++;
				}
				CheckMaterial (1);
				ChangeMaterial ();
			}
		}
	}

	void ChangeMaterial()
	{
		for (int i = 0; i < renderList.Length - 4; i++) {
			renderList [i].material = MatArray [MatIndex];
		}
	}

	void CheckMaterial(int direction)
	{
		while (!PlayerUpgrades.upgrades.mats.Contains (MatArray [MatIndex].color)) {
			if (MatIndex + direction < 0 || MatIndex + direction > 4) {
				if (MatIndex == 0 && direction == -1) {
					MatIndex = 4;
				} else if (MatIndex == 4 && direction == 1) {
					MatIndex = 0;
				}
			} else {
				MatIndex += direction;
			}
		}
	}

	/************************************/
	//GETTERS AND SETTERS
	/************************************/

	public Renderer[] RenderList
	{
		set{renderList = value;}
	}

	public Renderer GetRenderAtIndex(int i){
		return renderList [i];
	}

	public int MaterialIndex
	{
		get{return MatIndex;}
		set{
			if (value < renderList.Length) {
				MatIndex = value;
				ChangeMaterial ();
			}
		}
	}
}
