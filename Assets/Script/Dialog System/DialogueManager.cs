using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private UiDialogue uiDialogue;
    [SerializeField] private ConversationSO currentConversation;
    Queue queueDialogue = new Queue();
    public DialogSO currentDialogue;

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
        NextDialogue();
    }
    public void NextDialogue()
    {
        if (queueDialogue.Count > 0)
        {
            currentDialogue = queueDialogue.Dequeue() as DialogSO;
            uiDialogue.SetDialogue(currentDialogue);
        }
        else
        {
            EndDialogue();
        }
    }
    void EndDialogue()
    {
        switch (currentConversation.eventEndDialogue)
        {
            case EventGame.OpenConversation:
                gm.OpenConversation(currentConversation.conversation);
                break;
            case EventGame.OpenChoice:
                uiDialogue.ResetDialogue(false);
                gm.OpenChoice(currentConversation.choice);
                break;
            case EventGame.OpenCinematic:
                uiDialogue.ResetDialogue();
                gm.OpenCinematic(currentConversation.cinematic);
                break;
            case EventGame.OpenMap:
                uiDialogue.ResetDialogue();
                gm.OpenMap();
                break;
            case EventGame.OpenMinigame:
                uiDialogue.ResetDialogue();
                gm.OpenMinigame(currentConversation.minigameName);
                break;
            case EventGame.OpenNaration:
                uiDialogue.ResetDialogue();
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
