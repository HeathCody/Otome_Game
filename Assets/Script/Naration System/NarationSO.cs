using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NarationSO", menuName = "Scriptable Objects/NarationSO")]
public class NarationSO : ScriptableObject
{
    public string tittle;
    public List<NarationData> listNarationData;
    [EnumToggleButtons]
    public EventGame eventEndCinematic;
    [ShowIf("eventEndCinematic", EventGame.OpenConversation)] public ConversationSO conversation;
    [ShowIf("eventEndCinematic", EventGame.OpenChoice)] public ChoiceSO choice;
    [ShowIf("eventEndCinematic", EventGame.OpenCinematic)] public CinematicSO cinematic;
    [ShowIf("eventEndCinematic", EventGame.OpenNaration)] public NarationSO naration;
    [ShowIf("eventEndCinematic", EventGame.OpenMinigame)] public string minigameName;
    public EffectEvent effectstartEvent;
    public EffectEvent effectEndEvent;

    [System.Serializable]
    public class NarationData
    {
        public bool isHaveImage;
        [ShowIf("isHaveImage")] public Sprite sprBackground;
        [TextArea(2, 5)] public string Naration;
    }

}
