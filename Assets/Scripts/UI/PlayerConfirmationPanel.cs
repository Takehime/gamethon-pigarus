using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerConfirmationPanel : MonoBehaviour {
    [SerializeField] private int playerNumber;
    [SerializeField] private KeyCode buttonToConfirm;
    [SerializeField] private Image confirmIcon;
    //campo de cor
    public bool confirmed;

    public delegate void Confirm();
    public event Confirm ConfirmEvent;

    private void Start()
    {
        Reset();
    }
    public void Reset()
    {
        confirmed = false;
        int target = confirmed ? 1 : 0;
        confirmIcon.transform.DOScale(target, 0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(buttonToConfirm))
        {
            ToggleConfirm();
        }
    }
    private void ToggleConfirm()
    {
        confirmed = !confirmed;
        int target = confirmed ? 1 : 0;
        confirmIcon.transform.DOScale(target, 0.25f);
        ConfirmEvent();
    }

}
