using NUnit.Framework;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ConversationSO", menuName = "Scriptable Objects/ConversationSO")]
public class ConversationSO : ScriptableObject
{
    [Tooltip("Untuk penamaan dialog")]
    public string Judul;
    public Sprite sprBackgroundDialogue;
    public List<DialogSO> ListDialogue = new List<DialogSO>();
    [EnumToggleButtons]
    public EventGame eventEndDialogue;
    [ShowIf("eventEndDialogue", EventGame.OpenDIalogue)] public ConversationSO nextConversation;
    [ShowIf("eventEndDialogue", EventGame.OpenChoice)] public ChoiceSo openChoice;
    [ShowIf("eventEndDialogue", EventGame.OpenCinematic)] public Sprite spiteCinematic;
}
