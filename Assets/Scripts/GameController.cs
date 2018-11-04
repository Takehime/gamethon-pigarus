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
    public bool isGameOver = false;

    public List<GameObject> players = new List<GameObject>();
    public PlayerDatabase database;
    public GameObject worldCanvasMatchPoint;
    public GameObject audioSourcePrefab;
    public Image whiteScreen;

    public AudioClip loopClip;

	void Start () {
        Application.targetFrameRate = 30;
        
        database = PlayerDatabase.GetPlayerDatabase();

        victoryCounterContainer = GameObject.FindWithTag("victory counter container");
        InitializeVictoryCounter(victoryCounterContainer);

        Instantiate(audioSourcePrefab);
        var loopSong = Instantiate(audioSourcePrefab);
        loopSong.tag = "Untagged";
        var audioSource = loopSong.GetComponent<AudioSource>();
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();

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
        var mark = Instantiate(victoryCounterMarkPrefab);
        mark.transform.position = Camera.main.WorldToScreenPoint(FindPlayerByData(data).transform.position);
        mark.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        mark.GetComponent<Image>().color = new Color(data.color.r, data.color.g, data.color.b, 0.5f);
        mark.transform.SetAsFirstSibling();
        
        mark.transform.DOMove(worldCanvasMatchPoint.transform.position, 0.5f).SetEase(Ease.InFlash).OnComplete(() => {
            mark.GetComponent<Image>().DOFade(0f, 0.2f);
            var color = data.color;
            var image = victoryCounterMarkList[currentRound++].GetComponent<Image>();
            image.DOColor(color, victoryMarkAppearDuration);
            var imageOriginalScale = image.transform.localScale;
            image.transform.DOScale(new Vector3(imageOriginalScale.x * 1.3f, imageOriginalScale.y * 1.3f), 0.125f).OnComplete(() => {
                image.transform.DOScale(imageOriginalScale, 0.125f);
            });

            playerVictories[data.id]++;
            if (playerVictories[data.id] > Mathf.Floor(rounds / 2)) {
                isGameOver = true;
                Debug.Log("End game. Victory: player " + data.id);
                database.winner = data;
                database.loser = database.playerData[data.id == 0 ? 1 : 0];            
                FindPlayerByData(data).GetComponentInChildren<PlayerBehavior>().VictoryAnimation();
                StartCoroutine(TransitionToGameOver());
            }
            Destroy(mark.gameObject);
        });
    }

    GameObject FindPlayerByData(PlayerData data) {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
            if (player.GetComponentInChildren<PlayerBehavior>().data.id == data.id) {
                return player;
            }
        }

        return null;
    }

    public IEnumerator TransitionToGameOver() {
        yield return new WaitForSeconds(1.5f);
        whiteScreen.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.75f);
        Time.timeScale = 1f;
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
