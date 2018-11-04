using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : Menu {
    [SerializeField] private List<PlayerConfirmationPanel> playerPanels;

    bool gameStarted = false;

    public void Start()
    {
        group = GetComponent<CanvasGroup>();
        foreach (var p in playerPanels)
        {
            p.Reset();
            p.ConfirmEvent += CheckIfBothConfirmed;
        }
    }
    public override void OnShow()
    {
        foreach (var p in playerPanels)
            p.Reset();
    }
    public override void OnHide()
    {
        //desistir   
    }
    public void CheckIfBothConfirmed()
    {
        bool bothConfirmed = playerPanels[0].confirmed && playerPanels[1].confirmed;
        if (!gameStarted && bothConfirmed)
            StartCoroutine(StartGame());
    }
    private IEnumerator StartGame()
    {
        gameStarted = true;
        Transition.transition.SetBlockRaycast(true);
        yield return new WaitForSeconds(0.5f);
        Transition.transition.Show();
        yield return new WaitForSeconds(1f);
        print("ABRINDO SCENE");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
