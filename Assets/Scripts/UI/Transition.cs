using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Transition : MonoBehaviour {
    private const float DURATION = 0.25f;

    [SerializeField] private CanvasGroup iconGroup;
    private Image fillImage;

    public static Transition transition;

    private void Start()
    {
        fillImage = GetComponent<Image>();
        transition = this;
    }
    public virtual void Show() {
        fillImage.DOFillAmount(1, DURATION);
        iconGroup.DOFade(1, DURATION);
    }
    public virtual void Hide() {
        fillImage.DOFillAmount(0, DURATION);
        iconGroup.DOFade(0, DURATION);
    }
    public void SetBlockRaycast(bool b)
    {
        fillImage.raycastTarget = b;
    }
}
