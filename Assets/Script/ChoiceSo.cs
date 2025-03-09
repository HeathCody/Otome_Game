using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceSO", menuName = "Scriptable Objects/ChoiceSO")]
public class ChoiceSo : ScriptableObject
{
    public List<DataChoice> listChoice = new List<DataChoice>();
}

[Serializable]
public class DataChoice
{
    [FoldoutGroup("$ChoiceTittle")] public string ChoiceTittle;
    [FoldoutGroup("$ChoiceTittle")] public EventGame eventSelectChoice;
    [FoldoutGroup("$ChoiceTittle")] public List<DataReputation> listReputation = new List<DataReputation>();
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenDIalogue)] public ConversationSO nextConversation;
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenChoice)] public ChoiceSo openChoice;
    [FoldoutGroup("$ChoiceTittle")][ShowIf("eventSelectChoice", EventGame.OpenCinematic)] public Sprite sprCinematic;
}
[Serializable]
public class DataReputation
{
    [EnumToggleButtons]
    public BitMaskReputation reputation;
    public int pointReputation;
}
