using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonLoadGameData : MonoBehaviour
{
    private UiPanelLoadDataGame panelLoadGame;
    [SerializeField] private TextMeshProUGUI txtFileName;
    [SerializeField] private int indexButton;
    [SerializeField] private Button btnLoad;
    public void SetPanelLoad(UiPanelLoadDataGame panelload)
    {
        panelLoadGame = panelload;
    }
    public void SetButtonLoad(string filename, int index)
    {
        txtFileName.text = filename;
        indexButton = index;
    }
    public void ButtonLoad()
    {
        panelLoadGame.LoadDataGame(indexButton);
    }
}
