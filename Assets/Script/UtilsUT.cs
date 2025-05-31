using UnityEngine;

public enum MainMenuState { MainMenu, SavedFile, Setting, Gallery, GalleryImage }
public enum EventGame { None, OpenConversation, OpenChoice, OpenCinematic, OpenMap, OpenMinigame, OpenNaration }
public enum EffectEvent { None, FadeIn, FadeOut }
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
public class UtilsUT : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
