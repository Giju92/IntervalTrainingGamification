using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	AudioSource mAudio;

	// Use this for initialization
	void Start () {
		mAudio = GetComponent<AudioSource> ();		
	}
	
	public void PlaySound(){
		mAudio.Play ();
	}

	public void StopSound(){
		mAudio.Stop ();
	}

}
