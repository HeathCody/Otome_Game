using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPanelDataGame : MonoBehaviour
{
    [FoldoutGroup("Panel Data Game")][SerializeField] private List<PlayerData> listPlayerData => LoadSaveManager.instance.listPlayerData;
    [FoldoutGroup("Panel Data Game")][SerializeField] private GameObject panelLoadGameData;
    [FoldoutGroup("Panel Data Game")][SerializeField] private GameObject panelLoading;
/*    [FoldoutGroup("Panel Data Game")][SerializeField] private TextMeshProUGUI txtTittle;*/
    [FoldoutGroup("Panel Data Game")][SerializeField] private Button btnFirstPanelLoadGame;
    [FoldoutGroup("Panel Data Game")][SerializeField] private Toggle ToogleSaveGame;
    [FoldoutGroup("Panel Data Game")][SerializeField] private List<UiButtonGameData> listButtonData = new List<UiButtonGameData>();
    [FoldoutGroup("Panel Data Game")][SerializeField] private bool isMainMenu;
    [FoldoutGroup("Panel Data Game")][SerializeField] private bool isLoadGame;
    [FoldoutGroup("Panel Data Game")][SerializeField] private int indexPage;
    [FoldoutGroup("Panel Data Game")][SerializeField] private int MaxPlayerData;
    private int playerDataIndex;
    private int indexListPlayer;
    private int indexButton;
    void Start()
    {
        isLoadGame = false;
        indexPage = 0;
        ToogleSaveGame.interactable = isMainMenu ? false : true;
        panelLoading.SetActive(false);
        panelOverwrite.SetActive(false);
        panelLoadGameData.SetActive(false);
    }
    public void ToggleLoad(bool value)
    {
        if (!value) return;
        isLoadGame = true;
        /*txtTittle.text = "Load Game";*/
        ResetListButton();
        SetListButton(indexPage);
    }
    public void ToggleSave(bool value)
    {
        if (!value) return;
        isLoadGame = false;
        /*txtTittle.text = "Save Game";*/
        ResetListButton();
        SetListButton(indexPage);
    }
    public void SetIndexPage(int index)
    {
        indexPage = index;
        ResetListButton();
        SetListButton(indexPage);
    }
    public void OpenPanelLoadDataGame()
    {
        panelLoadGameData.SetActive(true);
        btnFirstPanelLoadGame.Select();
        ResetListButton();
        isLoadGame = isMainMenu ? true : isLoadGame;
        /*txtTittle.text = isLoadGame ? "Load Game" : "Save Game";*/
        SetListButton(indexPage);
    }
    public void ClosePanelLoadDataGame()
    {
        panelLoadGameData.SetActive(false);
    }
    private void ResetListButton()
    {
        for (int i = 0; i < listButtonData.Count; i++)
            listButtonData[i].gameObject.SetActive(false);
    }
    private void SetListButton(int page)
    {
        for (int i = 0; i < listButtonData.Count; i++)
        {
            playerDataIndex = (page * listButtonData.Count) + i;
            indexListPlayer = GetIndexListPlayer(playerDataIndex);
            if (indexListPlayer != -1)
                SetButtonData(i, listPlayerData[indexListPlayer]);
            else
                listButtonData[i].SetAsDefaultButton();
            if (playerDataIndex < MaxPlayerData)
                listButtonData[i].gameObject.SetActive(true);
        }
    }
    void SetButtonData(int indexbutton, PlayerData data)
    {
        listButtonData[indexbutton].SetButtonData(data.fileName);
    }
    int GetIndexListPlayer(int indexdata)
    {
        int index = -1;
        index = listPlayerData.FindIndex(x => x.indexData == indexdata);
        return index;
    }
    public void OpenButtonData(int indexbutton)
    {
        playerDataIndex = indexbutton + (indexPage * listButtonData.Count);
        indexListPlayer = GetIndexListPlayer(playerDataIndex);
        indexButton = indexbutton;
        if (isLoadGame)
        {
            if (indexListPlayer != -1)
                LoadGame(indexListPlayer);
            else
                Debug.Log("No Data");
        }
        else
        {
            if (indexListPlayer != -1)
                OverwriteData();
            else
                NewSaveGame();
        }
    }
    void LoadGame(int index)
    {
        LoadSaveManager.instance.LoadGamePlayerData(index);
        LoadSaveManager.instance.isFromLoadManager = true;
        SceneManager.LoadScene(1);
    }
    void OverwriteData()
    {
        panelOverwrite.SetActive(true);
    }
    void NewSaveGame()
    {
        panelLoading.SetActive(true);
        LoadSaveManager.instance.SaveGameData(playerDataIndex, true);
        StartCoroutine(IeLoadingLoadSave());
        indexListPlayer = GetIndexListPlayer(playerDataIndex);
        SetButtonData(indexButton, listPlayerData[indexListPlayer]);
    }

    #region Overwrite Game Data
    [FoldoutGroup("Panel Overwrite Game Data")][SerializeField] private GameObject panelOverwrite;
    public void ButtonConfirmOverwriteData()
    {
        panelOverwrite.SetActive(false);
        LoadSaveManager.instance.SaveGameData(playerDataIndex, false);
        StartCoroutine(IeLoadingLoadSave());
        SetButtonData(indexButton, listPlayerData[indexListPlayer]);
    }
    public void ButtonCancelOverwriteData()
    {
        panelOverwrite.SetActive(false);
    }
    #endregion
    IEnumerator IeLoadingLoadSave()
    {
        yield return new WaitUntil(() => LoadSaveManager.instance.onLoadSave == false);
        panelLoading.SetActive(false);
    }
}
