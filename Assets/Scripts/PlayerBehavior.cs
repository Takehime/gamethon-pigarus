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
	[SerializeField] private float downdashSpeed; 
	[SerializeField] private float movementSpeed; 
	
	[SerializeField] private Transform playerBegin;
	[SerializeField] private float deadTime;

	private Rigidbody2D rb; 
	private bool dead;

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (dead) {
			rb.velocity = Vector2.zero;
		}
		else {
			if (Input.GetKeyDown(ButtonDown)) {
				Dash(DashDirection.DOWN);
			}
			if (Input.GetKey(ButtonRight)) {
				Dash(DashDirection.RIGHT);
			}		
			if (Input.GetKey(ButtonLeft)) {
				Dash(DashDirection.LEFT);
			}
		}
	}
	
	void Dash(DashDirection dir) {
		Vector2 speedVector;
		switch (dir) {
			case DashDirection.DOWN:
				rb.velocity = new Vector2(rb.velocity.x, 0.0f);
				speedVector = new Vector2(0, -downdashSpeed*2);
				rb.AddForce(speedVector);
				break;
			case DashDirection.LEFT:
				rb.velocity = new Vector2(0,rb.velocity.y);			
				speedVector = new Vector2(-dashSpeed, 0);
				rb.AddForce(speedVector);				
				break;
			case DashDirection.RIGHT:
				rb.velocity = new Vector2(0,rb.velocity.y);			
				speedVector = new Vector2(dashSpeed, 0);
				rb.AddForce(speedVector);				
				break;	
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("bubble")) {
			rb.velocity = new Vector2(0.0f, 0.0f);
			rb.AddForce(new Vector2(0.0f, collision.gameObject.GetComponent<ObstaclesBehaviour>().BounceImpulse));
			Destroy(collision.gameObject);
		}
		if (collision.gameObject.CompareTag("floor")) {
			GetComponent<Rigidbody2D>().Sleep();
			GetComponent<BoxCollider2D>().enabled = false;
			Color color = GetComponent<SpriteRenderer>().color;
			color.a = color.a / 3 * 2;
			GetComponent<SpriteRenderer>().color = color;
			this.transform.position = playerBegin.position;
			StartCoroutine(Die());
		}
	}

	private IEnumerator Die() {
		dead = true;
		yield return new WaitForSeconds(deadTime);
		dead = false;
		Color color = GetComponent<SpriteRenderer>().color;
		print(color);
		color.a = 1f;
		GetComponent<SpriteRenderer>().color = color;
		GetComponent<Rigidbody2D>().WakeUp();
		GetComponent<BoxCollider2D>().enabled = true;
	}
}
