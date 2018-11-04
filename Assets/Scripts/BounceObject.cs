using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceObject : MonoBehaviour {
    Vector3 originalScale;

    [SerializeField] private float bounceForce;
    [SerializeField] Vector2 tweenFactor = new Vector2(1.5f, 0.8f);
    [SerializeField] float tweenSquishInDuration = 0.15f, tweenSquishOutDuration = 0.05f;

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
                rb.velocity = Vector2.zero;
                if (this.gameObject.tag == "Bird") {
                    rb.AddForce(Vector2.left * bounceForce);
                } else {
                    rb.AddForce(Vector2.up * bounceForce);           
                }
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
        print("why call this");
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
