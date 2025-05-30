using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IUIBase
{
    CanvasGroup GetCanvasGroup();
}

public class Efek : MonoBehaviour
{
    [SerializeField] private CanvasGroup UIGroupFadeIn;
    [SerializeField] private CanvasGroup UIGroupFadeOut;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public void FadeIn()
    {
        Debug.Log("fade in berhasil");
        fadeIn = true;
    }
    public void FadeOut()
    {
        Debug.Log("fade out berhasil");
        fadeOut = true;

    }
    public void Flash()
    {

    }

    public void InitFadeIn(IUIBase ui)
    {
        UIGroupFadeIn = ui.GetCanvasGroup();
    }
    
    public void InitFadeOut(IUIBase ui)
    {
        UIGroupFadeOut = ui.GetCanvasGroup();
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (UIGroupFadeIn.alpha < 1)
            {
                UIGroupFadeIn.alpha += Time.deltaTime;
                if (UIGroupFadeIn.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        
        if (fadeOut)
        {
            if (UIGroupFadeOut.alpha >= 0)
            {
                UIGroupFadeOut.alpha -= Time.deltaTime;
                if (UIGroupFadeOut.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
