using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiNaration : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private GameObject panelNaration;
    [SerializeField] private Image imgBackground;
    [SerializeField] private Color colorSemiBlack;
    [SerializeField] private TextMeshProUGUI txtNaration;
    [SerializeField] private Button btnNaration;
    private NarationSO currentNaration;
    int indexNaration;

    //efek
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float durationFade = 1f;

    void Awake()
    {
        panelNaration.SetActive(false);
    }
    public void OpenNaration(NarationSO naration)
    {
        currentNaration = naration;
        indexNaration = 0;
        if (currentNaration.listNarationData[indexNaration].isHaveImage)
        {
            imgBackground.sprite = currentNaration.listNarationData[indexNaration].sprBackground;
            imgBackground.color = colorSemiBlack;
        }
        else
        {
            imgBackground.sprite = null;
            imgBackground.color = Color.black;
        }
        txtNaration.text = currentNaration.listNarationData[indexNaration].Naration;
        panelNaration.SetActive(true);
        btnNaration.Select();
        switch (currentNaration.effectstartEvent)
        {
            case EffectEvent.None:
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                break;
            case EffectEvent.FadeIn:
                FadeIn();
                break;
        }
    }
    private void FadeIn()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(ieFade(true));
    }
    private void FadeOut()
    {
        canvasGroup.alpha = 1;
        StartCoroutine(ieFade(false));
    }
    IEnumerator ieFade(bool isFadeIn)
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
            panelNaration.SetActive(false);
        }
    }
    public void ButtonNaration()
    {
        if (currentNaration != null && indexNaration < currentNaration.listNarationData.Count - 1)
        {
            indexNaration++;
            if (currentNaration.listNarationData[indexNaration].isHaveImage)
            {
                imgBackground.sprite = currentNaration.listNarationData[indexNaration].sprBackground;
                imgBackground.color = colorSemiBlack;
            }
            else
            {
                imgBackground.sprite = null;
                imgBackground.color = Color.black;
            }
            txtNaration.text = currentNaration.listNarationData[indexNaration].Naration;
            return;
        }
        switch (currentNaration.effectEndEvent)
        {
            case EffectEvent.None:
                panelNaration.SetActive(false);
                EndNaration();
                break;
            case EffectEvent.FadeOut:
                EndNaration();
                FadeOut();
                break;
        }
    }

    void EndNaration()
    {
        if (currentNaration == null) return;
        switch (currentNaration.eventEndCinematic)
        {
            case EventGame.OpenConversation:
                gm.OpenConversation(currentNaration.conversation);
                break;
            case EventGame.OpenChoice:
                gm.OpenChoice(currentNaration.choice);
                break;
            case EventGame.OpenCinematic:
                gm.OpenCinematic(currentNaration.cinematic);
                break;
            case EventGame.OpenMap:
                gm.OpenMap();
                break;
            case EventGame.OpenMinigame:
                gm.OpenMinigame(currentNaration.minigameName);
                break;
            case EventGame.OpenNaration:
                gm.OpenNaration(currentNaration.naration);
                break;
        }
    }
}
