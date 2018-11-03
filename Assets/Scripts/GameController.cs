using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour {
    public static GameController GetGameController() {
        var gc = GameObject.FindGameObjectWithTag("GameController");
        if (gc == null || gc.GetComponentInChildren<GameController>() == null) {
            Debug.Break();
            print("GameController not found!");
            return null;
        } else {
            return gc.GetComponentInChildren<GameController>();
        }
    }

    [SerializeField] GameObject victoryCounterContainer, victoryCounterMarkPrefab;
    [SerializeField] int rounds = 5;
    public List<int> playerVictories = new List<int>();

    [SerializeField] float victoryMarkAppearDuration = 0.5f;

    List<GameObject> victoryCounterMarkList = new List<GameObject>();
    int currentRound = 0;

    public List<GameObject> players = new List<GameObject>();
    public PlayerDatabase database;

	void Start () {
        database = PlayerDatabase.GetPlayerDatabase();

        victoryCounterContainer = GameObject.FindWithTag("victory counter container");
        InitializeVictoryCounter(victoryCounterContainer);

        foreach (var pd in database.playerData) {
            playerVictories.Add(0);
            players[database.playerData.IndexOf(pd)].GetComponentInChildren<PlayerBehavior>().SetData(pd);
        }
	}
	
	void Update () {
	}

    void InitializeVictoryCounter(GameObject container) {
        if (container == null) {
            Debug.Break();
            print("victory counter not found");
        }

        for (int x = 1; x <= rounds; x++) {
            GameObject tmp = Instantiate(victoryCounterMarkPrefab);
            tmp.transform.SetParent(victoryCounterContainer.transform, false);
            victoryCounterMarkList.Add(tmp);
        }
    }

    void GiveRoundVictory(PlayerData data) {
        var color = data.color;
        victoryCounterMarkList[currentRound++].GetComponent<Image>().DOColor(color, victoryMarkAppearDuration);

        playerVictories[data.id]++;
        if (playerVictories[data.id] > Mathf.Floor(rounds / 2)) {
            Debug.Log("End game. Victory: player " + data.id);
            database.winner = data;
            database.loser = database.playerData[data.id == 0 ? 1 : 0];            
            StartCoroutine(TransitionToGameOver());
        }
    }

    public IEnumerator TransitionToGameOver() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Victory Scene");
    }

    public void GiveVictoryToOtherPlayer(int playerId) {
        foreach (var pd in database.playerData) {
            if (pd.id != playerId) {
                GiveRoundVictory(pd);
                return;
            }
        }
    }

    IEnumerator GameLoop() {
        yield break;
    }
}
