using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicBox : MonoBehaviour {

	static public MusicBox instance;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

	public void SetVolume(float vol)
	{
		GetComponent<AudioSource> ().volume = vol;
	}
}
