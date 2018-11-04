using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public int buttonIndex;

    public void SelectButton()
    {
        GetComponent<Button>().Select();
    }
    public void UnselectButton()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<MenuController>().Select(buttonIndex);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //
    }
}
