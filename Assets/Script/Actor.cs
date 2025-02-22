using UnityEngine;

public class Actor : MonoBehaviour
{
    public string Name;
    public Dialogue Dialogue;

    public void Start()
    {
        DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
    }
}
