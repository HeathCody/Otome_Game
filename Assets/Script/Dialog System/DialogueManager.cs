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
        NextDialogue();
    }
    public void NextDialogue()
    {
        uiDialogue.txtDialogue.text = string.Empty;
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
                efek.FadeIn();
                gm.OpenConversation(currentConversation.conversation);
                efek.FadeOut();
                break;
            case EventGame.OpenChoice:
                uiDialogue.ResetDialogue(false);
                gm.OpenChoice(currentConversation.choice);
                break;
            case EventGame.OpenCinematic:
                efek.FadeIn();
                uiDialogue.ResetDialogue();
                gm.OpenCinematic(currentConversation.cinematic);
                efek.FadeOut();
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
                efek.FadeIn();
                uiDialogue.ResetDialogue();
                gm.OpenNaration(currentConversation.naration);
                efek.FadeOut();
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
