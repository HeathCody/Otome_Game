using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiCinematic : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private GameObject panelCinematic;
    [SerializeField] private Image imgCinematic;
    [SerializeField] private Button btnCinematic;

    private CinematicSO currentCinematic;

    //efek
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float durationFade = 1f;
    void Awake()
    {
        panelCinematic.SetActive(false);
    }
    public void OpenCinematic(CinematicSO cinemaSo)
    {
        currentCinematic = cinemaSo;
        imgCinematic.sprite = currentCinematic.sprCinematic;
        panelCinematic.SetActive(true);
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
    IEnumerator IeFade(bool isFadeIn)
    {
        canvasGroup.interactable = false;
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
