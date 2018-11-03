﻿using System.Collections;
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
	private bool dead;

	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (dead) {
			rb.velocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("floor")) {
			StartCoroutine(Die());
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
