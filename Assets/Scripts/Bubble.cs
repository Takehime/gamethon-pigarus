using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : BounceObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnTouch() {
		Destroy(this.gameObject);
	}
}
