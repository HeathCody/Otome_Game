using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonGameData : MonoBehaviour
{
    [SerializeField] private UiPanelDataGame panelLoadGame;
    [SerializeField] private TextMeshProUGUI txtFileName;
    [SerializeField] private int indexButton;
    [SerializeField] private Button btnLoad;
    [SerializeField] private Image imgButton;
    [SerializeField] private Sprite sprDefaultButton;

    [Header("Thumbnail Screenshot")]
    [SerializeField] private RawImage rawThumbnail; // <- Tambahkan ini di Inspector

    public void SetButtonData(string filename)
    {
        txtFileName.text = filename;
    }

    public void ButtonOpenData()
    {
        panelLoadGame.OpenButtonData(indexButton);
    }

    public void SetAsDefaultButton()
    {
        txtFileName.text = "No Data";
        imgButton.sprite = sprDefaultButton;
        if (rawThumbnail != null)
            rawThumbnail.texture = null;
    }

    public void SetThumbnail(Texture2D tex)
    {
        if (rawThumbnail != null)
            rawThumbnail.texture = tex;
    }
}
