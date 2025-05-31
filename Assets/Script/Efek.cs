using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private UnityEvent eventOnFadeIn;

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
        UIGroupFadeIn.alpha = 0;
    }

    public void InitFadeOut(IUIBase ui)
    {
        UIGroupFadeOut = ui.GetCanvasGroup();
        UIGroupFadeOut.alpha = 1;
    }

    private void Update()
    {
        //     if (fadeIn)
        //     {
        //         if (UIGroupFadeIn.alpha < 1)
        //         {
        //             UIGroupFadeIn.alpha += Time.deltaTime;
        //             if (UIGroupFadeIn.alpha >= 1)
        //             {
        //                 fadeIn = false;
        //             }
        //         }
        //     }

        //     if (fadeOut)
        //     {
        //         if (UIGroupFadeOut.alpha >= 0)
        //         {
        //             UIGroupFadeOut.alpha -= Time.deltaTime;
        //             if (UIGroupFadeOut.alpha == 0)
        //             {
        //                 fadeOut = false;
        //             }
        //         }
        //     }
        // }
    }
    public IEnumerator IeFadeIn(CanvasGroup cvsgrp, float durationfade)
    {
        cvsgrp.interactable = false;
        float time = 0;
        while (time < durationfade)
        {
            cvsgrp.alpha = Mathf.Lerp(0, 1, time / durationfade);
            time += Time.deltaTime;
            yield return null;
        }
        cvsgrp.interactable = true;
    }
    public IEnumerator IeFadeOut(CanvasGroup cvsgrp, float durationfade)
    {
        cvsgrp.interactable = false;
        float time = 0;
        while (time < durationfade)
        {
            cvsgrp.alpha = Mathf.Lerp(1, 0, time / durationfade);
            time += Time.deltaTime;
            yield return null;
        }
        cvsgrp.interactable = false;
    }
}