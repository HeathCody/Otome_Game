using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private UiDialogue uiDialogue;
    [SerializeField] private ConversationSO currentConversation;
    [SerializeField] private float durationEffect = 1f;
    Queue queueDialogue = new Queue();
    public DialogSO currentDialogue;

    [SerializeField] private Efek efek;

    void Awake()
    {
        uiDialogue.ResetDialogue();
    }
    public void SetConversation(ConversationSO conversationSO)
    {
        uiDialogue.ResetDialogue();
        currentConversation = conversationSO;
        queueDialogue.Clear();
        foreach (DialogSO dialogue in currentConversation.ListDialogue)
        {
            dialogue.BgDialogue = currentConversation.sprBackgroundDialogue;
            queueDialogue.Enqueue(dialogue);
        }
        NextDialogue(currentConversation.effectStartEvent);
    }
    public void NextDialogue()
    {
        uiDialogue.txtDialogue.text = string.Empty;
        if (queueDialogue.Count > 0)
        {
            currentDialogue = queueDialogue.Dequeue() as DialogSO;
            uiDialogue.SetDialogue(currentDialogue, EffectEvent.None);
        }
        else
        {
            EndDialogue();
        }
    }
    public void NextDialogue(EffectEvent effect)
    {
        uiDialogue.txtDialogue.text = string.Empty;
        if (queueDialogue.Count > 0)
        {
            currentDialogue = queueDialogue.Dequeue() as DialogSO;
            uiDialogue.SetDialogue(currentDialogue, effect);
        }
        else
        {
            EndDialogue();
        }
    }
    void EndDialogue()
    {
        switch (currentConversation.effectEndEvent)
        {
            case EffectEvent.FadeOut:
                uiDialogue.EffectFadeOut();
                break;
            case EffectEvent.None:
                if (currentConversation.eventEndDialogue != EventGame.OpenConversation)
                    uiDialogue.ResetDialogue(currentConversation.eventEndDialogue == EventGame.OpenChoice ? false : true);
                break;
        }
        switch (currentConversation.eventEndDialogue)
        {
            case EventGame.OpenConversation:
                gm.OpenConversation(currentConversation.conversation);
                break;
            case EventGame.OpenChoice:
                gm.OpenChoice(currentConversation.choice);
                break;
            case EventGame.OpenCinematic:
                gm.OpenCinematic(currentConversation.cinematic);
                break;
            case EventGame.OpenMap:
                gm.OpenMap();
                break;
            case EventGame.OpenMinigame:
                gm.OpenMinigame(currentConversation.minigameName);
                break;
            case EventGame.OpenNaration:
                gm.OpenNaration(currentConversation.naration);
                break;
        }
    }
    [Title("Testing")]
    [Button(ButtonSizes.Medium)]
    public void ButtonTestingDialogue()
    {
        uiDialogue.ResetDialogue();
        queueDialogue.Clear();
        foreach (DialogSO dialogue in currentConversation.ListDialogue)
        {
            dialogue.BgDialogue = currentConversation.sprBackgroundDialogue;
            queueDialogue.Enqueue(dialogue);
        }
        NextDialogue();
    }
}
