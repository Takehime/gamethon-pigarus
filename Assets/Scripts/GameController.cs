using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class PlayerData {
    public Color color;
    public int id;
}

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
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    public List<int> playerVictories = new List<int>();

    [SerializeField] float victoryMarkAppearDuration = 0.5f;

    List<GameObject> victoryCounterMarkList = new List<GameObject>();
    int currentRound = 0;

    public List<GameObject> players = new List<GameObject>();

	void Start () {
        victoryCounterContainer = GameObject.FindWithTag("victory counter container");
        InitializeVictoryCounter(victoryCounterContainer);

        foreach (var pd in playerData) {
            playerVictories.Add(0);
            players[playerData.IndexOf(pd)].GetComponentInChildren<PlayerBehavior>().SetData(pd);
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
            Debug.Break();
        }
    }

    public void GiveVictoryToOtherPlayer(int playerId) {
        foreach (var pd in playerData) {
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
