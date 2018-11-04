using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour {

	[SerializeField] protected Vector2 velocity;
	[SerializeField] protected GameObject prefab;
	[SerializeField] protected float maxBounceImpulse;

	// Use this for initialization
	// virtual protected void Start() {

	// }

	public virtual void Spawn() {
		print("should be overrided");
	}

	protected virtual void Destroy() {
		Destroy(this.gameObject);
	}

	protected void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag("destroy")) {
			this.Destroy();
		}
	}
}
