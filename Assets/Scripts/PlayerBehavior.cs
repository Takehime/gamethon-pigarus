using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

	public GameObject victoryWing;
	public ParticleSystem victoryParticles;

	GameController gc;
	public bool hasWon = false;

	void Start () {
		gc = GameController.GetGameController();
		rb = this.GetComponent<Rigidbody2D>();
		allowCollideCount = allowCollideStartCount;
	}

	void FixedUpdate() {
		if (hasWon) {
			rb.velocity = Vector2.zero;
			return;
		}
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
		if (hasWon) {
			return;
		}
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
		var particles = this.GetComponentInChildren<ParticleSystem>();
		particles.Stop();

		GetComponent<Rigidbody2D>().Sleep();
		GetComponent<BoxCollider2D>().enabled = false;
		color.a = color.a / 3 * 2;
		var sr = GetComponent<SpriteRenderer>();
		sr.color = color;
		dead = true;

		this.transform.DOMoveY(this.transform.position.y - 1.5f, 1f);
		var originalRotation = this.transform.rotation;
		var rotate_tween = this.transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360);

		GameController.GetGameController().GiveVictoryToOtherPlayer(data.id);

		yield return new WaitForSeconds(1f);

		if (!gc.isGameOver) {
			this.transform.rotation = originalRotation;
			this.transform.position = playerBegin.position;

			var originalScale = this.transform.localScale;
			this.transform.localScale = Vector2.zero;

			this.transform.DOScale(originalScale, 0.5f).SetEase(Ease.InBounce);

			yield return new WaitForSeconds(0.5f);

			StartCoroutine(Revive());
		}
	}

	public IEnumerator Revive() {
		Color color = GetComponent<SpriteRenderer>().color;
		var particles = this.GetComponentInChildren<ParticleSystem>();
		particles.Play();

		// rotate_tween.Kill();

		dead = false;
		color.a = 1f;
		GetComponent<SpriteRenderer>().color = color;
		GetComponent<Rigidbody2D>().WakeUp();
		GetComponent<BoxCollider2D>().enabled = true;

		yield break;
	}

	public void VictoryAnimation() {
		hasWon = true;
		rb.velocity = Vector2.zero;
		this.transform.DOMoveY(this.transform.position.y + 1, 0.5f);
		victoryParticles.Play();
		victoryWing.SetActive(true);
		var sr = victoryWing.GetComponentInChildren<SpriteRenderer>();
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
		sr.DOFade(1f, 0.25f);
		var originalScale = victoryWing.transform.localScale;
		victoryWing.transform.DOScale(new Vector3(originalScale.x * 1.5f, originalScale.y * 1.5f, originalScale.z), 2f).SetEase(Ease.InExpo);
	}
}
