using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : ObstaclesBehaviour {

	float baseVelocity = -5.0f;

	// Use this for initialization
	void Start () {
		this.velocity.x = baseVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	 public new static void Spawn(GameObject baseObject) {
		 Instantiate(baseObject);
	}
}
