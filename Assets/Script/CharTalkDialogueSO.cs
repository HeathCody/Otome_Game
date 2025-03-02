using UnityEngine;

[CreateAssetMenu(fileName = "CharTalkDialogueSO", menuName = "Scriptable Objects/CharTalkDialogueSO")]
public class CharTalkDialogueSO : ScriptableObject
{
    [Header("TC_MC1_1")]
    [Tooltip("Untuk menentukan gambar talking character apa yang digunakan")]
    public Sprite SprTalkChar;

    [Tooltip("Untuk menentukan posisi talking character ada berada dimana")]
    public int TalkCharPos = 1;

    [Tooltip("Untuk menentukan apakah gambar ini sedang berbicara atau tidak")]
    public bool IsTalk;
}
