using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingItem : MonoBehaviour {

	public bool canCatch = false;

	void Start () {
		StartCoroutine(initialCooldown());
	}

	IEnumerator initialCooldown() {
		canCatch = false;
		var sr = this.GetComponentInChildren<SpriteRenderer>();
		sr.color = new Color(sr.color.r, sr.color.r, sr.color.b, 0.5f);
		yield return new WaitForSeconds(2f);
		sr.color = new Color(sr.color.r, sr.color.r, sr.color.b, 1f);
		canCatch = true;
	}

	public void Catch() { 
		Destroy(this.gameObject);
	}
}
