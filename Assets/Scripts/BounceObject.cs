using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceObject : MonoBehaviour {
    Vector3 originalScale;

    [SerializeField] private float maxBounceForce, bounceFactor = 1.2f;
    [SerializeField] Vector2 tweenFactor = new Vector2(1.5f, 0.8f);
    [SerializeField] float tweenSquishInDuration = 0.15f, tweenSquishOutDuration = 0.05f;

    public float BounceForce(Vector2 velocity, PlayerBehavior behaviour) {
        print(velocity + " --- " + behaviour.maxVelocity);
        print(maxBounceForce + " * " + (Mathf.Abs(velocity.y) / behaviour.maxVelocity.y) + " = " + maxBounceForce * (velocity.y / behaviour.maxVelocity.y));
		return maxBounceForce * (Mathf.Abs(velocity.magnitude) * bounceFactor / behaviour.maxVelocity.y);
	}

    void Start() {
        originalScale = this.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            var behaviour = collision.gameObject.GetComponent<PlayerBehavior>();
            if (behaviour.allowCollide) {
                Vector2 oldVelocity = rb.velocity;
                rb.velocity = Vector2.zero;
                // if (this.gameObject.tag == "Bird") {
                //     rb.AddForce(Vector2.left * BounceForce(oldVelocity, behaviour));
                // } else {
                    rb.AddForce(Vector2.up * BounceForce(oldVelocity, behaviour));           
                // }
                print("vou chamar o onTouch....");
                OnTouch();
                behaviour.allowCollide = false;
                // StartCoroutine(behaviour.StopCollide());
            }
        }
    }

    protected virtual void OnTouch()
    {
        // Destroy(this.gameObject);
        Squish();
    }

    public void Squish() {
        this.transform.DOScale(new Vector3(originalScale.x * tweenFactor.x, originalScale.y * tweenFactor.y), tweenSquishInDuration).OnComplete(() =>
        {
            this.transform.DOScale(originalScale, tweenSquishOutDuration);
        });
    }

    public void destroyBubble() {
        //trigger na animaçao
        Destroy(this.gameObject);
    }
}
