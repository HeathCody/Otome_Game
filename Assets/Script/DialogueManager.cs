using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public enum EventGame { None, OpenDIalogue, OpenChoice, OpenCinematic, OpenMap, OpenMinigame }
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private UiDialogue uiDialogue;
    [SerializeField] private ConversationSO currentConversation;
    Queue queueDialogue = new Queue();
    DialogSO currentDialogue;

    void Start()
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
            case EventGame.OpenDIalogue:
                gm.OpenConversation(currentConversation.nextConversation);
                break;
            case EventGame.OpenChoice:
                uiDialogue.ResetDialogue(false);
                gm.OpenChoice(currentConversation.openChoice);
                break;
            case EventGame.OpenCinematic:
                uiDialogue.ResetDialogue();
                gm.OpenCinematic(currentConversation.spiteCinematic);
                break;
            case EventGame.OpenMap:
                uiDialogue.ResetDialogue();
                gm.OpenMap();
                break;
            case EventGame.OpenMinigame:
                uiDialogue.ResetDialogue();
                gm.OpenMinigame();
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
