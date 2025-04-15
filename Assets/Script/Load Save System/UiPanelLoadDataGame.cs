using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPanelLoadDataGame : MonoBehaviour
{
    [SerializeField] private List<PlayerData> listPlayerData => LoadSaveManager.instance.listPlayerData;
    [SerializeField] private GameObject panelLoadGameData;
    [SerializeField] private Button btnFirstPanelLoadGame;
    [SerializeField] private GameObject prefabButtonLoadDataGame;
    [SerializeField] private Transform transListButton;
    [SerializeField] private List<UiButtonLoadGameData> listButtonLoadGame = new List<UiButtonLoadGameData>();
    public void OpenPanelLoadDataGame()
    {
        panelLoadGameData.SetActive(true);
        btnFirstPanelLoadGame.Select();
        ResetListButton();
        AddListButton();
        SetListButton();
    }
    public void ClosePanelLoadDataGame()
    {
        panelLoadGameData.SetActive(false);
    }
    private void ResetListButton()
    {
        for (int i = 0; i < listButtonLoadGame.Count; i++)
            listButtonLoadGame[i].gameObject.SetActive(false);
    }
    private void AddListButton()
    {
        if (listButtonLoadGame.Count < listPlayerData.Count)
        {
            for (int i = listButtonLoadGame.Count; i < listPlayerData.Count; i++)
            {
                GameObject prefabButton = Instantiate(prefabButtonLoadDataGame, transListButton);
                UiButtonLoadGameData btnLoadGame = prefabButton.GetComponent<UiButtonLoadGameData>();
                btnLoadGame.SetPanelLoad(this);
                prefabButton.SetActive(false);
                listButtonLoadGame.Add(btnLoadGame);
            }
        }
    }
    private void SetListButton()
    {
        for (int i = 0; i < listPlayerData.Count; i++)
        {
            int index = i;
            listButtonLoadGame[i].SetButtonLoad(listPlayerData[i].fileName, index);
            listButtonLoadGame[i].gameObject.SetActive(true);
        }
    }
    public void LoadDataGame(int index)
    {
        LoadSaveManager.instance.LoadGamePlayerData(index);
        LoadSaveManager.instance.isFromLoadManager = true;
        SceneManager.LoadScene(1);
    }
}
