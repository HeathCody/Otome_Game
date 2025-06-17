using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class UiCinematic : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private GameObject panelCinematic;
    [SerializeField] private Image imgCinematic;
    [SerializeField] private VideoPlayer vidCinematic;
    [SerializeField] private Button btnCinematic;

    public CinematicSO currentCinematic;

    //efek
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float durationFade = 1f;
    [SerializeField] private Image panelImageTransition;
    void Awake()
    {
        panelCinematic.SetActive(false);
    }
    public void OpenCinematic(CinematicSO cinemaSo)
    {
        currentCinematic = cinemaSo;
        //cek apakah video ato sprite
        if(currentCinematic.sprCinematic != null)
        {
            imgCinematic.sprite = currentCinematic.sprCinematic;
        }
        else if (currentCinematic.videoClip != null)
        {
            vidCinematic.clip = currentCinematic.videoClip;
            vidCinematic.Play();
        }
        panelCinematic.SetActive(true);
        MusicManager.Instance.PlayBacksound();
        MusicManager.Instance.PlaySFX();
        btnCinematic.Select();
        switch (currentCinematic.effectStartEvent)
        {
            case EffectEvent.None:
                canvasGroup.interactable = true;
                canvasGroup.alpha = 1;
                break;
            case EffectEvent.FadeIn:
                canvasGroup.alpha = 0;
                EffectFadeIn();
                break;
            case EffectEvent.FadeFromBlack:
                canvasGroup.alpha = 0;
                EffectFadeFromBlack();
                break;
            case EffectEvent.FlashIn:
                canvasGroup.alpha = 0;
                EffectFlashIn();
                break;
        }
    }
    private void EffectFadeIn()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(IeFade(true));
    }
    public void EffectFadeOut()
    {
        canvasGroup.alpha = 1;
        StartCoroutine(IeFade(false));
    }
    //in
    public void EffectFadeFromBlack()
    {
        canvasGroup.alpha = 0;
        StartFade(Color.black, 0, 1, CloseCinematic);
    }
    //out
    public void EffectFadeToBlack()
    {
        canvasGroup.alpha = 1;
        StartFade(Color.black, 1, 0);
    }
    public void EffectFlashIn()
    {
        imgCinematic.sprite = null;
        canvasGroup.alpha = 0;
        StartFade(Color.white, 0, 1, CloseCinematic);
    }
    public void EffectFlashOut()
    {
        imgCinematic.sprite = null;
        canvasGroup.alpha = 1;
        StartFade(Color.white, 1, 0);
    }
    IEnumerator IeFade(bool isFadeIn)
    {
        //canvasGroup.interactable = false;
        float time = 0;
        while (time < durationFade)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, isFadeIn ? 1 : 0, time / durationFade);
            time += Time.deltaTime;
            yield return null;
        }
        if (isFadeIn)
            canvasGroup.interactable = true;
        else
        {
            panelCinematic.SetActive(false);
            if (currentCinematic != null && currentCinematic.isCinematicUnlock)
                gm.UnlockGallery(currentCinematic.tittle);
        }
    }

    

    private void StartFade(Color fadeColor, float fromAlpha, float toAlpha, Action onComplete = null)
    {
        panelCinematic.SetActive(true);
        canvasGroup.alpha = fromAlpha;
        panelImageTransition.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fromAlpha);
        StartCoroutine(IeFadeFlash(fadeColor, fromAlpha, toAlpha, onComplete));
    }

    private IEnumerator IeFadeFlash(Color fadeColor, float fromAlpha, float toAlpha, Action onComplete)
    {
        float time = 0;
        while (time < durationFade)
        {
            float t = time / durationFade;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, t);
            canvasGroup.alpha = alpha;
            panelImageTransition.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        // Final values to avoid precision errors
        canvasGroup.alpha = toAlpha;
        panelImageTransition.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, toAlpha);

        if (toAlpha == 0)
        {
            panelCinematic.SetActive(false);
            if (currentCinematic != null && currentCinematic.isCinematicUnlock)
                gm.UnlockGallery(currentCinematic.tittle);
        }
        else
        {
            canvasGroup.interactable = true;
        }
        // Panggil callback jika ada
        onComplete?.Invoke();
    }

    public void CloseCinematic()
    {
        switch (currentCinematic.effectEndEvent)
        {
            case EffectEvent.None:
                panelCinematic.SetActive(false);
                EndCinematic();
                if (currentCinematic != null && currentCinematic.isCinematicUnlock)
                    gm.UnlockGallery(currentCinematic.tittle);
                break;
            case EffectEvent.FadeOut:
                EndCinematic();
                EffectFadeOut();
                break;
            case EffectEvent.FadeToBlack:
                EndCinematic();
                EffectFadeToBlack();
                break;
            case EffectEvent.FlashOut:
                EndCinematic();
                EffectFlashOut();
                break;
        }
    }

    void EndCinematic()
    {
        if (currentCinematic == null) return;
        switch (currentCinematic.eventEndCinematic)
        {
            case EventGame.OpenConversation:
                gm.OpenConversation(currentCinematic.nextConversation);
                break;
            case EventGame.OpenChoice:
                gm.OpenChoice(currentCinematic.openChoice);
                break;
            case EventGame.OpenCinematic:
                gm.OpenCinematic(currentCinematic.cinematic);
                break;
            case EventGame.OpenMap:
                gm.OpenMap();
                break;
            case EventGame.OpenMinigame:
                gm.OpenMinigame(currentCinematic.minigameName);
                break;
            case EventGame.OpenNaration:
                gm.OpenNaration(currentCinematic.naration);
                break;
        }
    }
}
