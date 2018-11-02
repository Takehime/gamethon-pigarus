using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashDirection{
	DOWN, RIGHT, LEFT
}

public class PlayerBehavior : MonoBehaviour {

	[SerializeField] private KeyCode ButtonDown; 
	[SerializeField] private KeyCode ButtonRight; 
	[SerializeField] private KeyCode ButtonLeft;
	[SerializeField] private float dashSpeed; 
	[SerializeField] private float movementSpeed; 

	private Rigidbody2D rb; 

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (Input.GetKeyDown(ButtonDown)) {
			Dash(DashDirection.DOWN);
		}
		if (Input.GetKeyDown(ButtonRight)) {
			Dash(DashDirection.RIGHT);
		}		
		if (Input.GetKeyDown(ButtonLeft)) {
			Dash(DashDirection.LEFT);
		}
	}
	
	void Dash(DashDirection dir) {
		Vector2 speedVector;
		switch (dir) {
			case DashDirection.DOWN:
				rb.velocity = new Vector2(0,0);
				speedVector = new Vector2(0, -dashSpeed*2);
				rb.AddForce(speedVector);
				break;
			case DashDirection.LEFT:
				rb.velocity = new Vector2(0,0);			
				speedVector = new Vector2(-dashSpeed, 0);
				rb.AddForce(speedVector);				
				break;
			case DashDirection.RIGHT:
				rb.velocity = new Vector2(0,0);			
				speedVector = new Vector2(dashSpeed, 0);
				rb.AddForce(speedVector);				
				break;	
		}
	}

}
