using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class DialogueNode
{
    public List<string> dialogueText = new List<string>();

    public bool NeedResponse = false;

    public List<DialogueResponse> responses;

    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}
