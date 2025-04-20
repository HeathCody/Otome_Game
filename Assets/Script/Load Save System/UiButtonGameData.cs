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
        txtFileName.name = "No Data";
        imgButton.sprite = sprDefaultButton;
    }
}
