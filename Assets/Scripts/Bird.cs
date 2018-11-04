using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BounceObject {

	public AudioClip birdSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnTouch() {
		AudioSource audio = AudioSourceController.GetAudioSourceController().GetComponent<AudioSource>();
		audio.PlayOneShot(birdSound);
	}

}
