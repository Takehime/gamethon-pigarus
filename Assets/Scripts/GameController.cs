using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class PlayerData {
    public Color color;
}

public class GameController : MonoBehaviour {

    [SerializeField] GameObject victoryCounterContainer, victoryCounterMarkPrefab;
    [SerializeField] int rounds = 5;
    [SerializeField] List<PlayerData> playerData = new List<PlayerData>();
    [SerializeField] float victoryMarkAppearDuration = 0.5f;

    List<GameObject> victoryCounterMarkList = new List<GameObject>();
    int currentRound = 0;

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

    void GiveRoundVictory(int playerId) {
        var color = playerData[playerId].color;
        victoryCounterMarkList[currentRound++].GetComponent<Image>().DOColor(color, victoryMarkAppearDuration);
    }

    IEnumerator GameLoop() {
        yield break;
    }

	// Use this for initialization
	void Start () {
        victoryCounterContainer = GameObject.FindWithTag("victory counter container");
        InitializeVictoryCounter(victoryCounterContainer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
