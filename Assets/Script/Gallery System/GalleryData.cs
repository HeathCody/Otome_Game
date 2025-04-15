using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GalleryData
{
    [Tooltip("Masukkan judul Image-nya")]
    public string tittle;

    [Tooltip("Menentukan apakah Image sudah terbuka")]
    public bool isOpen;
    public GalleryData(string tittleData, bool isOpenData)
    {
        tittle = tittleData;
        isOpen = isOpenData;
    }
}
[System.Serializable]
public class GameDataGallery
{
    [Tooltip("List Gallery Data")]
    public List<GalleryData> listGalleryData = new List<GalleryData>();
}
