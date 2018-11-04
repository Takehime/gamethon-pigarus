using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashDirection{
	DOWN, RIGHT, LEFT
}

public class PlayerBehavior : MonoBehaviour {
	
	public PlayerData data;

	[SerializeField] private Transform playerBegin;
	[SerializeField] private float deadTime;

	private Rigidbody2D rb; 
	[HideInInspector] public bool dead;
	public bool allowCollide = true;
	public int allowCollideStartCount = 5;
	private int allowCollideCount = 5;
	public Vector2 maxVelocity;

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		allowCollideCount = allowCollideStartCount;
	}

	void FixedUpdate() {
		if (!allowCollide) {
			if (allowCollideCount > 0) {
				allowCollideCount--;
			} else {
				allowCollide = true;
				allowCollideCount = allowCollideStartCount;
			}
		}
		if (Mathf.Abs(rb.velocity.x) > maxVelocity.x) {
			rb.velocity = new Vector2(
				(rb.velocity.x > 0) ? maxVelocity.x : -maxVelocity.x,
				rb.velocity.y);
		}
		if (Mathf.Abs(rb.velocity.y) > maxVelocity.y) {
			rb.velocity = new Vector2(
				rb.velocity.x,
				(rb.velocity.y > 0) ? maxVelocity.y : -maxVelocity.y);			
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("floor")) {
			if (allowCollide) {
				allowCollide = false;
				StartCoroutine(Die());
			}
		}
	}

	public void SetData(PlayerData data) {
		this.data = data;
		this.GetComponentInChildren<SpriteRenderer>().color = data.color;
	}

	private IEnumerator Die() {
		Color color = GetComponent<SpriteRenderer>().color;

		GetComponent<Rigidbody2D>().Sleep();
		GetComponent<BoxCollider2D>().enabled = false;
		color.a = color.a / 3 * 2;
		GetComponent<SpriteRenderer>().color = color;
		this.transform.position = playerBegin.position;
		dead = true;

		GameController.GetGameController().GiveVictoryToOtherPlayer(data.id);
		
		yield return new WaitForSeconds(deadTime);

		dead = false;
		color.a = 1f;
		GetComponent<SpriteRenderer>().color = color;
		GetComponent<Rigidbody2D>().WakeUp();
		GetComponent<BoxCollider2D>().enabled = true;
	}
}
