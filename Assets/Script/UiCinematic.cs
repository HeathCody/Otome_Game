using UnityEngine;
using UnityEngine.UI;

public class UiCinematic : MonoBehaviour
{
    [SerializeField] private GameObject panelCinematic;
    [SerializeField] private Image imgCinematic;
    void Start()
    {
        CloseCinematic();
    }
    public void OpenCinematic(Sprite sprCinematic)
    {
        imgCinematic.sprite = sprCinematic;
        panelCinematic.SetActive(true);
    }
    public void CloseCinematic()
    {
        panelCinematic.SetActive(false);
    }
}
