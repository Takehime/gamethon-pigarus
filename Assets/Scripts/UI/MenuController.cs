using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    [SerializeField] private MenuSlideManager slideManager;

    [SerializeField] private List<MenuButton> buttons;
    [SerializeField] private int selectedButton;

    [SerializeField] private List<CanvasGroup> groups;

    public static bool selectionLock;

    private void Start()
    {
        InitializeButton();
    }
    private void Update()
    {
        if (!selectionLock)
        {
            /*
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Select(Mathf.Clamp(selectedButton + 1, 0, buttons.Count - 1));
            }else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Select(Mathf.Clamp(selectedButton - 1, 0, buttons.Count - 1));
            }
            */
        }
    }
    private void InitializeButton()
    {
        foreach (MenuButton button in buttons)
            button.UnselectButton();
        buttons[selectedButton].SelectButton();
        slideManager.OpenSlide(selectedButton);
    }
    public void Select(int index)
    {
        if (selectedButton == index)
            return;
        buttons[selectedButton].UnselectButton();
        selectedButton = index;
        buttons[selectedButton].SelectButton();
        slideManager.OpenSlide(selectedButton);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
