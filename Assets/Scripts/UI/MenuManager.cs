using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour {
    private const float LOWER_PANEL_INITIAL_POSITION = -275f;
    private const float LOWER_PANEL_FINAL_POSITION = -465f;

    [SerializeField] private RectTransform lowerPanel;
    [SerializeField] private CanvasGroup fadePanel;

    private int currentMenu = 0;
    [SerializeField] private List<Menu> menus; 

    private void Start()
    {
        HideAll();
    }
    public void HideAll()
    {
        foreach( Menu m in menus)
            m.Hide();
    }
    public void OpenMenu(int index)
    {
        fadePanel.DOFade(1, 0.5f);
        lowerPanel.DOAnchorPosY(LOWER_PANEL_FINAL_POSITION, 0.25f);
        currentMenu = index;
        menus[currentMenu].Show();
    }
    public void Hide(float duration = 0.25f)
    {
        fadePanel.DOFade(0, 0.5f);
        lowerPanel.DOAnchorPosY(LOWER_PANEL_INITIAL_POSITION, duration);
        menus[currentMenu].Hide();
    }
}
