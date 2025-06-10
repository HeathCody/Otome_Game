using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource backsoundSrc;
    public AudioSource SFXSrc;

    public AudioClip backsoundMainMenu;
    public DialogueManager dialogueManager;

    private void Start()
    {
        PlayBacksoundMainMenu();
    }
    public void PlayBacksoundMainMenu()
    {
        backsoundSrc.clip = backsoundMainMenu;
        backsoundSrc.Play();
    }
    public void PlayBacksound()
    {
        backsoundSrc.clip = dialogueManager.currentConversation.Backsound;
        backsoundSrc.Play();
    }
    public void PlaySFX()
    {
        SFXSrc.PlayOneShot(dialogueManager.currentDialogue.SFXMusic);
    }
}
