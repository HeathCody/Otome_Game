using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour, ILoadSaveObjects
{
    [FoldoutGroup("New Game")][SerializeField] string newGameEventTittle;
    [FoldoutGroup("New Game")][SerializeField] EventGame newGameEventGame;
    [FoldoutGroup("Gameplay Manager")][ReadOnly][SerializeField] EventGame currentEventGame;
    [FoldoutGroup("Gameplay Manager")][ReadOnly][SerializeField] string currentEventTittle;
    [FoldoutGroup("Gameplay Manager")][SerializeField] ListGameEvent listGameEvent;
    [FoldoutGroup("Gameplay Manager")][SerializeField] DialogueManager dialogueManager;
    [FoldoutGroup("Gameplay Manager")][SerializeField] UIChoice uIChoice;
    [FoldoutGroup("Gameplay Manager")][SerializeField] UiCinematic uiCinematic;
    [FoldoutGroup("Gameplay Manager")][SerializeField] UiNaration uiNaration;
    [FoldoutGroup("Gameplay Manager")][SerializeField] UiPanelGallery uiGallery;
    [FoldoutGroup("Gameplay Manager")][SerializeField] UiPanelDataGame uiLoadSave;
    [FoldoutGroup("Gameplay Manager")][SerializeField] PlayerData playerData;
    [FoldoutGroup("Gameplay Manager")][SerializeField] private int ReputationNetral;
    [FoldoutGroup("Gameplay Manager")][SerializeField] private int ReputationChar1;
    [FoldoutGroup("Gameplay Manager")][SerializeField] private int ReputationChar2;
    [FoldoutGroup("Gameplay Manager")][SerializeField] private int ReputationChar3;
    [FoldoutGroup("Gameplay Manager")][SerializeField] private int ReputationChar4;
    [FoldoutGroup("Gameplay Manager")][SerializeField] private int ReputationChar5;
    private void Awake()
    {
        LoadSaveManager.Register(this);
    }
    private void OnDestroy()
    {
        LoadSaveManager.Unregister(this);
    }
    public void LoadGameData(PlayerData dataPlayer, GameDataGallery dataGallery)
    {
        currentEventTittle = dataPlayer.currentEventTittle;
        currentEventGame = dataPlayer.currentEventGame;
        ReputationNetral = dataPlayer.ReputationNetral;
        ReputationChar1 = dataPlayer.ReputationChar1;
        ReputationChar2 = dataPlayer.ReputationChar2;
        ReputationChar3 = dataPlayer.ReputationChar3;
        ReputationChar4 = dataPlayer.ReputationChar4;
        ReputationChar5 = dataPlayer.ReputationChar5;
    }

    public void SaveGameData(ref PlayerData data, ref GameDataGallery dataGallery)
    {
        data.currentEventTittle = currentEventTittle;
        data.currentEventGame = currentEventGame;
        data.ReputationNetral = ReputationNetral;
        data.ReputationChar1 = ReputationChar1;
        data.ReputationChar2 = ReputationChar2;
        data.ReputationChar3 = ReputationChar3;
        data.ReputationChar4 = ReputationChar4;
        data.ReputationChar5 = ReputationChar5;
    }
    private void Start()
    {
        if (!LoadSaveManager.instance.isFromLoadManager) return;
        LoadSaveManager.instance.isFromLoadManager = false;
        LoadSaveManager.instance.LoadObjectData();
        if (string.IsNullOrEmpty(currentEventTittle))
        {
            currentEventTittle = newGameEventTittle;
            currentEventGame = newGameEventGame;
        }
        int index = -1;
        switch (currentEventGame)
        {
            case EventGame.OpenConversation:
                index = listGameEvent.listGameConversation.FindIndex(x => x.tittle == currentEventTittle);
                if (index == -1)
                {
                    Debug.LogError("Current Event Tittle cant find the event : " + currentEventTittle);
                    return;
                }
                ConversationSO conversation = listGameEvent.listGameConversation[index].conversation;
                
                OpenConversation(conversation);
                break;
            case EventGame.OpenCinematic:
                index = listGameEvent.listCinematic.FindIndex(x => x.tittle == currentEventTittle);
                if (index == -1)
                {
                    Debug.LogError("Current Event Tittle cant find the event : " + currentEventTittle);
                    return;
                }
                CinematicSO cinematic = listGameEvent.listCinematic[index].cinematic;
                OpenCinematic(cinematic);
                break;
            case EventGame.OpenChoice:
                index = listGameEvent.listGameChoice.FindIndex(x => x.tittle == currentEventTittle);
                if (index == -1)
                {
                    Debug.LogError("Current Event Tittle cant find the event : " + currentEventTittle);
                    return;
                }
                ChoiceSO choice = listGameEvent.listGameChoice[index].choice;
                OpenChoice(choice);
                break;
            case EventGame.OpenNaration:
                index = listGameEvent.listNaration.FindIndex(x => x.tittle == currentEventTittle);
                if (index == -1)
                {
                    Debug.LogError("Current Event Tittle cant find the event : " + currentEventTittle);
                    return;
                }
                NarationSO blackscreenDialogue = listGameEvent.listNaration[index].naration;
                OpenNaration(blackscreenDialogue);
                break;
            case EventGame.OpenMinigame:
                OpenMinigame(currentEventTittle);
                break;
            case EventGame.OpenMap:
                OpenMap();
                break;
        }
    }
    public void OpenConversation(ConversationSO conversation)
    {
        currentEventGame = EventGame.OpenConversation;
        currentEventTittle = conversation.tittle;
        Debug.Log("Open Conversation : " + currentEventTittle);
        dialogueManager.SetConversation(conversation);
    }
    public void OpenChoice(ChoiceSO choice)
    {
        currentEventGame = EventGame.OpenChoice;
        currentEventTittle = choice.tittle;
        Debug.Log("Open Choice : " + currentEventTittle);
        uIChoice.OpenChoice(choice);
    }
    public void OpenCinematic(CinematicSO cinematic)
    {
        currentEventGame = EventGame.OpenCinematic;
        currentEventTittle = cinematic.tittle;
        Debug.Log("Open Cinematic : " + currentEventTittle);
        uiCinematic.OpenCinematic(cinematic);
    }
    public void OpenMap()
    {
        currentEventGame = EventGame.OpenMap;
        // currentEventTittle = cinematic.tittle;
        Debug.Log("Open Map");
    }
    public void OpenMinigame(string tittle)
    {
        currentEventGame = EventGame.OpenMinigame;
        currentEventTittle = tittle;
        Debug.Log("Open Minigame " + tittle);
    }
    public void OpenNaration(NarationSO naration)
    {
        currentEventGame = EventGame.OpenNaration;
        currentEventTittle = naration.tittle;
        Debug.Log("Open Naration : " + currentEventTittle);
        uiNaration.OpenNaration(naration);
    }
    public void UnlockGallery(string judul)
    {
        uiGallery.UnlockGallery(judul);
    }
    public void AddReputation(List<DataReputation> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if ((BitMaskReputation.Netral & data[i].reputation) == BitMaskReputation.Netral)
            {
                ReputationNetral += data[i].pointReputation;
            }
            if ((BitMaskReputation.Char1 & data[i].reputation) == BitMaskReputation.Char1)
            {
                ReputationChar1 += data[i].pointReputation;
            }
            if ((BitMaskReputation.Char2 & data[i].reputation) == BitMaskReputation.Char2)
            {
                ReputationChar2 += data[i].pointReputation;
            }
            if ((BitMaskReputation.Char3 & data[i].reputation) == BitMaskReputation.Char3)
            {
                ReputationChar3 += data[i].pointReputation;
            }
        }
    }
    #region Ui Menu
    public void OpenDialogueLog()
    {
        Debug.Log("Open Dialogue Log");
    }
    public void OpenSetting()
    {
        Debug.Log("Open Setting");
    }
    public void BackToMainMenu()
    {
        Debug.Log("Back To Main Menu");
        SceneManager.LoadScene(0);
    }
    #endregion
}
