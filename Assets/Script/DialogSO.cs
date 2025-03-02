using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO", menuName = "Scriptable Objects/DialogSO")]
public class DialogSO : ScriptableObject
{
    [Header("Penamaannya Prolog_Rumah_Sakit")]
    [Tooltip("Untuk penamaan percakapan")]
    public string Judul;

    [Header("1/2/3")]
    [Tooltip("Untuk penamaan berada di percakapan berapa")]
    public int Section;
    
    [Tooltip("Untuk menentukan background yang digunakan saat dialog")]
    public Sprite BgDialogue;

    [Tooltip("Untuk menentukan 2D character apa saja yang muncul")]
    public List<CharTalkDialogueSO> ListCharTalk = new List<CharTalkDialogueSO>();

    [Tooltip("Untuk menentukan nama character yang berbicara")]
    public string CharName;

    public string Dialogue;
}
