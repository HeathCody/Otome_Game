using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource backsoundSrc;
    public AudioSource SFXSrc;

    public AudioClip backsoundMainMenu;
    public DialogueManager dialogueManager;
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
            dialogueManager = GameObject.Find("GameplayManager").GetComponent<DialogueManager>();
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
        backsoundSrc.clip = dialogueManager.currentConversation.Backsound;
        backsoundSrc.Play();
    }
    public void PlaySFX()
    {
        SFXSrc.PlayOneShot(dialogueManager.currentDialogue.SFXMusic);
    }
}
