using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] UIChoice uIChoice;
    [SerializeField] UiCinematic uiCinematic;
    public TextMeshProUGUI txtPointNetral;
    public int ReputationNetral;
    public TextMeshProUGUI txtPointChar1;
    public int ReputationChar1;
    public TextMeshProUGUI txtPointChar2;
    public int ReputationChar2;
    public TextMeshProUGUI txtPointChar3;
    public int ReputationChar3;
    public void OpenConversation(ConversationSO conversation)
    {
        dialogueManager.SetConversation(conversation);
    }
    public void OpenChoice(ChoiceSo choice)
    {
        uIChoice.OpenChoice(choice);
    }
    public void OpenCinematic(Sprite sprCinematic)
    {
        uiCinematic.OpenCinematic(sprCinematic);
        Debug.Log("Open Cinematic");
    }
    public void OpenMap()
    {
        Debug.Log("Open Map");
    }
    public void OpenMinigame()
    {
        Debug.Log("Open Minigame");
    }
    public void AddReputation(List<DataReputation> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if ((BitMaskReputation.Netral & data[i].reputation) == BitMaskReputation.Netral)
            {
                ReputationNetral += data[i].pointReputation;
                txtPointNetral.text = ReputationNetral.ToString();
            }
            if ((BitMaskReputation.Char1 & data[i].reputation) == BitMaskReputation.Char1)
            {
                ReputationChar1 += data[i].pointReputation;
                txtPointChar1.text = ReputationChar1.ToString();
            }
            if ((BitMaskReputation.Char2 & data[i].reputation) == BitMaskReputation.Char2)
            {
                ReputationChar2 += data[i].pointReputation;
                txtPointChar2.text = ReputationChar2.ToString();
            }
            if ((BitMaskReputation.Char3 & data[i].reputation) == BitMaskReputation.Char3)
            {
                ReputationChar3 += data[i].pointReputation;
                txtPointChar3.text = ReputationChar3.ToString();
            }
        }
    }
}
