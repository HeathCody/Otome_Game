using UnityEngine;

public interface ILoadSaveObjects
{
    void LoadGameData(PlayerData dataPlayer, GameDataGallery dataGallery);
    void SaveGameData(ref PlayerData data, ref GameDataGallery dataGallery);
}
