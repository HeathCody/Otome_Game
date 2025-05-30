using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiNaration : MonoBehaviour, IUIBase
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
    [SerializeField] private Efek efek;
    [SerializeField] private CanvasGroup canvasGroup;
    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }

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
        efek.InitFadeIn(this);
        efek.FadeIn();
        panelNaration.SetActive(true);
        btnNaration.Select();
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
        efek.InitFadeOut(this);
        efek.FadeOut();
        panelNaration.SetActive(false);
        EndNaration();
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
