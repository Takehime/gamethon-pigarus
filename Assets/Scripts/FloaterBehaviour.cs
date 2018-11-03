using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterBehaviour : ObstaclesBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
	
	 public static void Spawn(GameObject baseObject, Vector2 baseVelocity) {
		GameObject newFloater = Instantiate(baseObject);
		newFloater.GetComponent<FloaterBehaviour>().velocity = baseVelocity; 
	}
}
