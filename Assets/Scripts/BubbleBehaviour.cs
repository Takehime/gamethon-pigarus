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

    public static void Spawn(GameObject baseObject, Vector2 baseVelocity, Vector3 startPosition) {
		GameObject newBubble = Instantiate(baseObject);
		newBubble.GetComponent<BubbleBehaviour>().velocity = baseVelocity;
        newBubble.transform.position = startPosition;
	}
}
