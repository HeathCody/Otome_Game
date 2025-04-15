using UnityEngine;
using UnityEngine.UI;

public class UiCinematic : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private GameObject panelCinematic;
    [SerializeField] private Image imgCinematic;
    [SerializeField] private Button btnCinematic;
    private CinematicSO currentCinematic;
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
    }
    public void CloseCinematic()
    {
        panelCinematic.SetActive(false);
        EndCinematic();
        if (currentCinematic != null && currentCinematic.isCinematicUnlock)
            gm.UnlockGallery(currentCinematic.tittle);
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
