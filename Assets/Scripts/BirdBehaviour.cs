using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : ObstaclesBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}


	public static void Spawn(GameObject baseObject, Vector2 baseVelocity, Vector3 startPosition) {
		GameObject newBird = Instantiate(baseObject);
		newBird.GetComponent<BirdBehaviour>().velocity = baseVelocity;
		newBird.transform.position = startPosition;
	}
}
