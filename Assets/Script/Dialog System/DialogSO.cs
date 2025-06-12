using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO", menuName = "Scriptable Objects/DialogSO")]
public class DialogSO : ScriptableObject
{
    [Tooltip("Untuk penamaan percakapan")]
    public string Judul;
    [Tooltip("Untuk penamaan berada di percakapan berapa")]
    public int Section;
    [Tooltip("Untuk menentukan background yang digunakan saat dialog")]
    [HideInInspector] public Sprite BgDialogue;
    [Tooltip("Untuk menentukan 2D character apa saja yang muncul")]
    public List<CharTalk> ListCharTalk = new List<CharTalk>();
    [Tooltip("Untuk menentukan nama character yang berbicara")]
    public string CharName;
    [TextArea(2, 5)] public string Dialogue;
    public Sprite SprBoxChar;
    public AudioClip SFXMusic;
}
[Serializable]
public class CharTalk
{
    [Header("TC_MC1_1")]
    [Tooltip("Untuk menentukan gambar talking character apa yang digunakan")]
    public Sprite SprTalkChar;

    [Tooltip("Untuk menentukan posisi talking character ada berada dimana")]
    public int TalkCharPos = 1;

    [Tooltip("Untuk menentukan apakah gambar ini sedang berbicara atau tidak")]
    public bool IsTalk;
    public bool isAnimated;
    public string anim;
}