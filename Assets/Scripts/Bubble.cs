using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : BounceObject {

	public AudioClip bubbleSound;
	public AudioClip bubbleSound2;
	public float soundVolume = 1.0f, soundVolume2 = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnTouch() {
		print("onTouch Bubble");
		AudioSource audio = AudioSourceController.GetAudioSourceController().GetComponent<AudioSource>();
		audio.PlayOneShot(bubbleSound, soundVolume);
		audio.PlayOneShot(bubbleSound2, soundVolume2);
		Destroy(this.gameObject);
	}
}
