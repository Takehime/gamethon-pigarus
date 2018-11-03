using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour {

	public TextMeshProUGUI victoryText;
	public PlayerDatabase database;

	void Start () {
		database = PlayerDatabase.GetPlayerDatabase();
		victoryText.text = "WINNER: #" + database.winner.id;
	}
}
