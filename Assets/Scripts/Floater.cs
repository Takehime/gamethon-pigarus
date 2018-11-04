using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : BounceObject {

	public AudioClip floaterSound;
	public float soundVolume;

	// Use this for initialization
	void Start () {
		originalScale = this.transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnTouch() {
		Squish();
		AudioSource audio = AudioSourceController.GetAudioSourceController().GetComponent<AudioSource>();
		audio.PlayOneShot(floaterSound, soundVolume);
	}
}
