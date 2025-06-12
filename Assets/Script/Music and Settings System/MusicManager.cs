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
        PlayBacksoundMainMenu();
        
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
        switch (gameplayManager.currentEventGame)
        {
            case EventGame.OpenConversation:
                backsoundSrc.clip = dialogueManager.currentConversation.Backsound;
                break;
            case EventGame.OpenNaration:
                backsoundSrc.clip = uiNaration.currentNaration.Backsound;
                break;
            case EventGame.OpenCinematic:
                backsoundSrc.clip = uiCinematic.currentCinematic.Backsound;
                break;
        }
        
        backsoundSrc.Play();
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
