using Sirenix.OdinInspector;
using UnityEngine;

public class UiGameplay : MonoBehaviour
{
    [FoldoutGroup("UI Gameplay")][SerializeField] private GameObject panelMenuLong;
    [FoldoutGroup("UI Gameplay")][SerializeField] private GameObject panelDialog;
    [FoldoutGroup("UI Gameplay")][SerializeField] private GameObject panelMenuOpen;
    [FoldoutGroup("UI Gameplay")][SerializeField] private GameplayManager gm;
    [FoldoutGroup("UI Gameplay")][SerializeField] public bool isDialogActive = true;
    void Start()
    {
        panelMenuLong.SetActive(false);
    }
    public void ButtonMenuOpen()
    {
        panelMenuLong.SetActive(true);
    }
    public void ButtonMenuClose()
    {
        panelMenuLong.SetActive(false);
    }
    public void ButtonDialogueLog()
    {
        gm.OpenDialogueLog();
    }
    public void ButtonSetting()
    {
        gm.OpenSetting();
    }
    public void ButtonBackToMainMenu()
    {
        gm.BackToMainMenu();
    }

    public void ButtonCloseDialog()
    {
        panelDialog.SetActive(false);
        panelMenuOpen.SetActive(false);
        isDialogActive = false;
    }
    public void ButtonOpenDialog()
    {
        panelDialog.SetActive(true);
        panelMenuOpen.SetActive(true);
        isDialogActive = true;
    }
}
