using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSO", menuName = "Scriptable Objects/ConversationSO")]
public class ConversationSO : ScriptableObject
{
    [Tooltip("Untuk penamaan dialog")]
    public string tittle;
    public Sprite sprBackgroundDialogue;
    public List<DialogSO> ListDialogue = new List<DialogSO>();
    [EnumToggleButtons]
    public EventGame eventEndDialogue;
    [ShowIf("eventEndDialogue", EventGame.OpenConversation)] public ConversationSO conversation;
    [ShowIf("eventEndDialogue", EventGame.OpenChoice)] public ChoiceSO choice;
    [ShowIf("eventEndDialogue", EventGame.OpenCinematic)] public CinematicSO cinematic;
    [ShowIf("eventEndDialogue", EventGame.OpenNaration)] public NarationSO naration;
    [ShowIf("eventEndDialogue", EventGame.OpenMinigame)] public string minigameName;
}
