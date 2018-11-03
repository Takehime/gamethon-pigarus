using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloaterBehaviour : ObstaclesBehaviour {

    Vector3 originalScale;
    [SerializeField] Vector2 tweenFactor = new Vector2(1.5f, 0.8f);
    [SerializeField] float tweenSquishInDuration = 0.15f, tweenSquishOutDuration = 0.05f;

	// Use this for initialization
	void Start () {
        originalScale = this.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
	
	public static void Spawn(GameObject baseObject, Vector2 baseVelocity, Vector3 startPosition) {
		GameObject newFloater = Instantiate(baseObject);
		newFloater.GetComponent<FloaterBehaviour>().velocity = baseVelocity;
        newFloater.transform.position = startPosition;
	}
}
