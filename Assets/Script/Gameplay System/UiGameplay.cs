using Sirenix.OdinInspector;
using UnityEngine;

public class UiGameplay : MonoBehaviour
{
    [FoldoutGroup("UI Gameplay")][SerializeField] private GameObject panelMenuLong;
    [FoldoutGroup("UI Gameplay")][SerializeField] private GameplayManager gm;
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
}
