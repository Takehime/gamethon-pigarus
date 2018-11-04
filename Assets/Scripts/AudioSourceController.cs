using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour {


    public static AudioSourceController GetAudioSourceController() {
        var gc = GameObject.FindGameObjectWithTag("AudioSource");
        if (gc == null || gc.GetComponentInChildren<AudioSourceController>() == null) {
            Debug.Break();
            print("AudioSourceController not found!");
            return null;
        } else {
            return gc.GetComponentInChildren<AudioSourceController>();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
