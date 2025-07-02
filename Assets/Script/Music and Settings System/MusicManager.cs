using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource backsoundSrc;
    public AudioSource SFXSrc;

    public AudioClip backsoundMainMenu;
    public GameplayManager gameplayManager;
    public DialogueManager dialogueManager;
    public UiNaration uiNaration;
    public UiCinematic uiCinematic;

    public UISettings uiSettings;

    //instance
    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat
        }
    }

    private void Start()
    {        
        // Inisialisasi slider dengan volume AudioSource saat start
        uiSettings.musicVolumeSld.value = backsoundSrc.volume;
        uiSettings.musicVolumeSld.onValueChanged.AddListener(UpdateVolumeMusic);
        // Inisialisasi slider dengan volume AudioSource saat start
        uiSettings.soundVolumeSld.value = SFXSrc.volume;
        uiSettings.soundVolumeSld.onValueChanged.AddListener(UpdateVolumeSound);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
            dialogueManager = GameObject.Find("GameplayManager").GetComponent<DialogueManager>();
            uiNaration = GameObject.Find("Ui-Naration").GetComponent<UiNaration>();
            uiCinematic = GameObject.Find("Ui-Cinematic").GetComponent<UiCinematic>();
        }
        
        if (scene.name == "Main-Menu")
        {
            backsoundSrc.Stop();
            PlayBacksoundMainMenu();
        }
    }

    public void UpdateVolumeMusic(float value)
    {
        backsoundSrc.volume = value;
    }
    
    public void UpdateVolumeSound(float value)
    {
        SFXSrc.volume = value;
    }

    public void PlayBacksoundMainMenu()
    {
        backsoundSrc.clip = backsoundMainMenu;
        backsoundSrc.Play();
    }
    public void PlayBacksound()
    {
        AudioClip newClip = null;

        switch (gameplayManager.currentEventGame)
        {
            case EventGame.OpenConversation:
                newClip = dialogueManager.currentConversation.Backsound;
                break;
            case EventGame.OpenNaration:
                newClip = uiNaration.currentNaration.Backsound;
                break;
            case EventGame.OpenCinematic:
                newClip = uiCinematic.currentCinematic.Backsound;
                break;
        }

        if (newClip != null && newClip != backsoundSrc.clip)
        {
            StartCoroutine(FadeToNewClip(newClip, 1f)); // 1f = durasi fade (bisa kamu ubah)
        }
    }


    IEnumerator FadeToNewClip(AudioClip newClip, float duration)
    {
        float startVolume = backsoundSrc.volume;

        // Fade Out
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            backsoundSrc.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        backsoundSrc.volume = 0;
        backsoundSrc.Stop();
        backsoundSrc.clip = newClip;
        backsoundSrc.Play();

        // Fade In
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            backsoundSrc.volume = Mathf.Lerp(0, startVolume, t / duration);
            yield return null;
        }

        backsoundSrc.volume = startVolume;
    }


    public void PlaySFX()
    {
        switch (gameplayManager.currentEventGame)
        {
            case EventGame.OpenConversation:
                SFXSrc.PlayOneShot(dialogueManager.currentDialogue.SFXMusic);
                break;
            case EventGame.OpenNaration:
                SFXSrc.PlayOneShot(uiNaration.currentNaration.SFXMusic);
                break;
            case EventGame.OpenCinematic:
                SFXSrc.PlayOneShot(uiCinematic.currentCinematic.SFXMusic);
                break;
        }
    }
}
