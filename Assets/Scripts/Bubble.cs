using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : BounceObject {

	public AudioClip bubbleSound;
	public AudioClip bubbleSound2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool alreadyTouched = false;

	protected override void OnTouch() {
		if (alreadyTouched) {
			return;
		}

		alreadyTouched = true;

		print("onTouch Bubble");
		AudioSource audio = AudioSourceController.GetAudioSourceController().GetComponent<AudioSource>();
		audio.PlayOneShot(bubbleSound, 3f);
		// audio.PlayOneShot(bubbleSound2);
		this.GetComponentInChildren<Animator>().SetTrigger("bubblepop");
		// Destroy(this.gameObject);
	}
}
