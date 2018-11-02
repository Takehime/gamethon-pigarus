using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

	[SerializeField] protected Vector2 velocity;

	// Use this for initialization
	// virtual protected void Start() {

	// }

	virtual protected void Spawn() {

	}

	virtual protected void Destroy() {
		Destroy(this.gameObject);
	}

	protected void OnCollisionEnter2D(Collision2D col) {
		print(col.gameObject.tag);
		if (col.gameObject.CompareTag("destroy")) {
			this.Destroy();
		}
	}
}
