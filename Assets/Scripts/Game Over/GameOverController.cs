using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverController : MonoBehaviour {

	public TextMeshProUGUI victoryText;
	public PlayerDatabase database;

	public Image winnerImage, loserImage;
	public Image winnerImage_2;

	public GameObject photo;
	public Image whitescreen;
	public CanvasGroup canvasGroup;

	void Start() {
		StartCoroutine(Init());
		database = PlayerDatabase.GetPlayerDatabase();
		victoryText.text = "PLAYER " + (database.winner.id + 1) + " is the WINNER";
		winnerImage.color = database.winner.color;
		winnerImage_2.color = database.winner.color;
		loserImage.color = database.loser.color;
	}

	IEnumerator Init() {
		whitescreen.DOFade(0f, 0.2f);
		photo.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
		yield return new WaitForSeconds(1f);
		canvasGroup.DOFade(1f, 0.5f);
	}

}
