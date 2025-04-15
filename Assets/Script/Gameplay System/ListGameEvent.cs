using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ListGameEvent : MonoBehaviour
{
    public List<EventConversation> listGameConversation;
    public List<EventChoice> listGameChoice;
    public List<EventCinematic> listCinematic;
    public List<EventNaration> listNaration;

}
[System.Serializable]
public class EventConversation
{
    [FoldoutGroup("$tittle")] public string tittle;
    [FoldoutGroup("$tittle")] public ConversationSO conversation;
}
[System.Serializable]
public class EventChoice
{
    [FoldoutGroup("$tittle")] public string tittle;
    [FoldoutGroup("$tittle")] public ChoiceSO choice;
}
[System.Serializable]
public class EventCinematic
{
    [FoldoutGroup("$tittle")] public string tittle;
    [FoldoutGroup("$tittle")] public CinematicSO cinematic;
}
[System.Serializable]
public class EventNaration
{
    [FoldoutGroup("$tittle")] public string tittle;
    [FoldoutGroup("$tittle")] public NarationSO naration;
}
