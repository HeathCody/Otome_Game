using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Cinematic SO", menuName = "Scriptable Objects/Cinematic SO")]
public class CinematicSO : ScriptableObject
{
    public string tittle;
    public Sprite sprCinematic;
    [EnumToggleButtons]
    public EventGame eventEndCinematic;
    public bool isCinematicUnlock;
    [ShowIf("eventEndCinematic", EventGame.OpenConversation)] public ConversationSO nextConversation;
    [ShowIf("eventEndCinematic", EventGame.OpenChoice)] public ChoiceSO openChoice;
    [ShowIf("eventEndCinematic", EventGame.OpenCinematic)] public CinematicSO cinematic;
    [ShowIf("eventEndCinematic", EventGame.OpenNaration)] public NarationSO naration;
    [ShowIf("eventEndCinematic", EventGame.OpenMinigame)] public string minigameName;
}
