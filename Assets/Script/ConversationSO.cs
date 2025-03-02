using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSO", menuName = "Scriptable Objects/ConversationSO")]
public class ConversationSO : ScriptableObject
{
    [Header("Penamaannya Prolog_Rumah_Sakit")]
    [Tooltip("Untuk penamaan dialog")]
    public string Judul;

    List<DialogSO> ListDialogue= new List<DialogSO>();

    /*public abstract EndConversation()
    {

    }*/
}
