using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdBehaviour : ObstaclesBehaviour {

	public float duration = 0.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine(UpDown());
	}

	public IEnumerator UpDown() {
		var rb = GetComponent<Rigidbody2D>();
		bool aux = false;
		while (true) {
			aux = !aux;
			var tween = DOTween.To(() => (Vector3) GetComponent<Rigidbody2D>().velocity, x => GetComponent<Rigidbody2D>().velocity = x, new Vector3(velocity.x, (aux ? -1 : 1) * velocity.y), duration);
			yield return new WaitForSeconds(duration);
		}
	}

	public static void Spawn(GameObject baseObject, Vector2 baseVelocity, Vector3 startPosition) {
		GameObject newBird = Instantiate(baseObject);
		newBird.GetComponent<BirdBehaviour>().velocity = baseVelocity;
		newBird.transform.position = startPosition;
	}
}
