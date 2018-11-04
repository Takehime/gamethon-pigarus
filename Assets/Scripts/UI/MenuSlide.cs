using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class MenuSlide : MonoBehaviour {
    private const float POSITION_Y_OFFSET = 720f*1.5f;
    private const float INITIAL_POSITION = 0f;
    private const float TIME_TO_MOVE = 0.25f;

    [SerializeField] private Image background;
    [SerializeField] private RectTransform elementsRoot;

    public virtual void Show(int bias, float duration = TIME_TO_MOVE)
    {
        background.DOFade(1, duration);
        elementsRoot.DOAnchorPosY(INITIAL_POSITION + POSITION_Y_OFFSET * bias, 0f);
        elementsRoot.DOAnchorPosY(INITIAL_POSITION, duration);
        elementsRoot.DOScale(0f, 0f);
        elementsRoot.DOScale(1f, duration);
    }
    public virtual void Hide(int bias, float duration = TIME_TO_MOVE)
    {
        background.DOFade(0, duration);
        elementsRoot.DOAnchorPosY(INITIAL_POSITION, 0f);
        elementsRoot.DOAnchorPosY(INITIAL_POSITION + POSITION_Y_OFFSET * bias, duration);
        //elementsRoot.DOScale(0f, duration);
    }
}
