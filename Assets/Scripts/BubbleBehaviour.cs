using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : Obstacles {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
}
