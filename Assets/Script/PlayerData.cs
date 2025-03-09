using UnityEngine;
[System.Flags]
public enum BitMaskReputation
{
    Netral = 1 << 0,
    Char1 = 1 << 1,
    Char2 = 1 << 2,
    Char3 = 1 << 3,
    Char4 = 1 << 4,
    Char5 = 1 << 5
}
public class PlayerData : MonoBehaviour
{
    public string currentDialogue;
    public string currentSection;
}
