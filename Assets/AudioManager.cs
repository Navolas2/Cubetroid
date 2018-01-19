using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	[HideInInspector]
	public static AudioManager Singleton;

	public List<Sound> sounds;

	public string OnStartSound = "";
	// Use this for initialization
	private void Awake () {
		if (Singleton == null) {
			Singleton = this;
			DontDestroyOnLoad (this);
		} else 	if(Singleton != this){
			Destroy (this);
		}
		SetUpAudio ();
	}

	private void SetUpAudio(){
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.name = s.name;
			s.source.loop = s.loop;
		}
	}

	public void PlaySound(string sound_name){
		Sound s = sounds.Find (delegate(Sound obj) {
			return obj.name == sound_name;
		});
		if (s != null) {
			s.source.Play ();
		}
	}

	public void StopSound(string sound_name){
		Sound s = sounds.Find (delegate(Sound obj) {
			return obj.name == sound_name;
		});
		if (s != null) {
			s.source.Stop();
		}
	}

	public bool IsPlaying(string sound_name){
		Sound s = sounds.Find (delegate(Sound obj) {
			return obj.name == sound_name;
		});
		return s.source.isPlaying;
	}

	void Start(){
		if (OnStartSound != "") {
			PlaySound (OnStartSound);
		}
	}
}
