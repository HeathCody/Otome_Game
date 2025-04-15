using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceSO", menuName = "Scriptable Objects/ChoiceSO")]
public class ChoiceSO : ScriptableObject
{
    public string tittle;
    public List<DataChoice> listChoice = new List<DataChoice>();
}

[Serializable]
public class DataChoice
{
    [FoldoutGroup("$ChoiceTittle")] public string ChoiceTittle;
    [EnumToggleButtons]
    [FoldoutGroup("$ChoiceTittle")] public EventGame eventSelectChoice;
    [FoldoutGroup("$ChoiceTittle")] public List<DataReputation> listReputation = new List<DataReputation>();
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenConversation)] public ConversationSO conversation;
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenChoice)] public ChoiceSO choice;
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenCinematic)] public CinematicSO cinematic; [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenNaration)] public NarationSO naration;
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenMinigame)] public string minigameName;
}
[Serializable]
public class DataReputation
{
    [EnumToggleButtons]
    public BitMaskReputation reputation;
    public int pointReputation;
}
