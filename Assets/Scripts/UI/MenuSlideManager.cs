using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlideManager : MonoBehaviour {

    [SerializeField] private List<MenuSlide> slides;
    [SerializeField] private int currentSlide;

    private void Start()
    {
        InitializeMenuSlides();
    }
    private void InitializeMenuSlides()
    {
        foreach(var s in slides)
        {
            s.Hide(-1, 0);
        }
        slides[currentSlide].Show(1,0);
    }
    public void OpenSlide(int slideIndex)
    {
        if (currentSlide == slideIndex)
            return;
        int bias = Mathf.Clamp(currentSlide - slideIndex, -1, 1);
        slides[currentSlide].Hide(-bias);
        currentSlide = slideIndex;
        slides[currentSlide].Show(bias);
    }
}
