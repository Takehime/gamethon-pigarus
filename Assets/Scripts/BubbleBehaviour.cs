using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : ObstaclesBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	 public static void Spawn(GameObject baseObject, float baseVelocity) {
		GameObject newBubble = Instantiate(baseObject);
		newBubble.GetComponent<BubbleBehaviour>().velocity.x = baseVelocity; 
	}
}
