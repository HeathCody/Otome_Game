using System.Collections;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [FoldoutGroup("Panel Main Menu")][SerializeField] private GameObject panelMainMenu;
    [FoldoutGroup("Panel Main Menu")][SerializeField] private Button btnFirstMainMenu;
    [FoldoutGroup("Panel Main Menu")][SerializeField] private MainMenuState menuState;
    [FoldoutGroup("Panel Main Menu")][SerializeField] private bool dataIsReady => LoadSaveManager.instance.dataIsReady;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => dataIsReady);
        ResetAllPanel();
        OpenMainMenu();
    }
    void ResetAllPanel()
    {
        CloseMainMenu();
        uiLoadDataGame.ClosePanelLoadDataGame();
        uiGallery.CloseGallery();
        uiGallery.CloseGalleryImage();
    }
    void OpenMainMenu()
    {
        menuState = MainMenuState.MainMenu;
        panelMainMenu.SetActive(true);
        btnFirstMainMenu.Select();
    }
    void CloseMainMenu()
    {
        panelMainMenu.SetActive(false);
    }
    public void BackState()
    {
        switch (menuState)
        {
            case MainMenuState.SavedFile:
                uiLoadDataGame.ClosePanelLoadDataGame();
                OpenMainMenu();
                break;
            case MainMenuState.Gallery:
                uiGallery.CloseGallery();
                OpenMainMenu();
                break;
            case MainMenuState.GalleryImage:
                uiGallery.CloseGalleryImage();
                menuState = MainMenuState.Gallery;
                uiGallery.OpenGallery();
                break;
            case MainMenuState.Setting:
                uiSettings.CloseSettings();
                OpenMainMenu();
                break;
        }
    }
    public void SetMainState(MainMenuState state)
    {
        menuState = state;
    }

    #region Gameplay
    [FoldoutGroup("Gameplay")][SerializeField] private UiPanelDataGame uiLoadDataGame;
    public void ButtonNewGame()
    {
        LoadSaveManager.instance.NewGameData();
        LoadSaveManager.instance.isFromLoadManager = true;
        CloseMainMenu();
        SceneManager.LoadScene(1);
    }
    public void ButtonLoadGame()
    {
        CloseMainMenu();
        menuState = MainMenuState.SavedFile;
        uiLoadDataGame.OpenPanelLoadDataGame();
    }
    #endregion
    #region Gallery
    [FoldoutGroup("Gallery")][SerializeField] private UiPanelGallery uiGallery;
    public void ButtonGallery()
    {
        CloseMainMenu();
        menuState = MainMenuState.Gallery;
        uiGallery.OpenGallery();
    }
    #endregion
    #region Setting
    [FoldoutGroup("Settings")][SerializeField] private UISettings uiSettings;
    public void ButtonSetting()
    {
        CloseMainMenu();
        menuState = MainMenuState.Setting;
        uiSettings.OpenSettings();
    }
    #endregion
    public void ButtonQuit()
    {
        Application.Quit();
    }

}
