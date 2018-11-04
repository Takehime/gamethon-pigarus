using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public TextMeshProUGUI victoryText;
	public PlayerDatabase database;

	public Image winnerImage, loserImage;

	void Start() {
		database = PlayerDatabase.GetPlayerDatabase();
		victoryText.text = "PLAYER " + (database.winner.id + 1) + " is the WINNER";
		winnerImage.color = database.winner.color;
		loserImage.color = database.loser.color;
	}

}
