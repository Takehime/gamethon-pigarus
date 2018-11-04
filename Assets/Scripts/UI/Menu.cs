using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu : MonoBehaviour {
    protected CanvasGroup group;
    public List<RectTransform> elements;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
    }
    public void Show() {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
        for(int i = 0; i < elements.Count; i++)
        {
            float currentPos = elements[i].anchoredPosition.x;
            elements[i].DOAnchorPosX(currentPos - 300f, 0);
            elements[i].DOAnchorPosX(currentPos, 0.35f + i * 0.1f);
        }
        OnShow();
    }
    public void Hide() {
        if (group == null) return;
        group.DOFade(0, 0);
        group.interactable = false;
        group.blocksRaycasts = false;
        OnHide();
    }
    public virtual void OnShow() { }
    public virtual void OnHide() { }
}
