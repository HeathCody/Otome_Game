using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UiPanelGallery : MonoBehaviour, ILoadSaveObjects
{
    private void Awake()
    {
        LoadSaveManager.Register(this);
    }
    private void OnDestroy()
    {
        LoadSaveManager.Unregister(this);
    }
    #region Panel Gallery
    [FoldoutGroup("Panel Gallery")][SerializeField] private MainMenuManager mainMenuManager;
    [FoldoutGroup("Panel Gallery")][SerializeField] private GameObject panelGallery;
    [FoldoutGroup("Panel Gallery")][SerializeField] private GameObject panelListButtonGallery;
    [FoldoutGroup("Panel Gallery")][SerializeField] private Button btnFirstListGallery;
    [FoldoutGroup("Panel Gallery")][SerializeField] private GameDataGallery galleryData;
    [FoldoutGroup("Panel Gallery")][SerializeField] private List<UiButtonGalleryData> listButtonGallery;
    public void OpenGallery()
    {
        panelGallery.SetActive(true);
        panelListButtonGallery.SetActive(true);
        panelGalleryImage.SetActive(false);
        btnFirstListGallery.Select();
    }
    public void CloseGallery()
    {
        panelGallery.SetActive(false);
        panelListButtonGallery.SetActive(true);
        panelGalleryImage.SetActive(false);
    }
    void SetButtonGallery()
    {
        for (int i = 0; i < galleryData.listGalleryData.Count; i++)
        {
            int index = listButtonGallery.FindIndex((x) => x.tittle == galleryData.listGalleryData[i].tittle);
            if (index != -1)
                listButtonGallery[index].isUnlock = galleryData.listGalleryData[i].isOpen;
        }
        for (int i = 0; i < listButtonGallery.Count; i++)
            listButtonGallery[i].SetButtonGallery();
    }
    public void UnlockGallery(string tittle)
    {
        int index = galleryData.listGalleryData.FindIndex((x) => x.tittle == tittle);
        if (index != -1)
            galleryData.listGalleryData[index].isOpen = true;
        else
            galleryData.listGalleryData.Add(new GalleryData(tittle, true));
        int indexButton = listButtonGallery.FindIndex((x) => x.tittle == tittle);
        if (indexButton != -1)
        {
            listButtonGallery[indexButton].isUnlock = true;
            listButtonGallery[indexButton].SetButtonGallery();
        }
    }
    #endregion
    #region Gallery Image
    [FoldoutGroup("Gallery Image")][SerializeField] private GameObject panelGalleryImage;
    [FoldoutGroup("Gallery Image")][SerializeField] private Image imgGallery;
    [FoldoutGroup("Gallery Image")][SerializeField] private Button btnFirstGalleryImage;
    public void OpenGalleryImage(int index)
    {
        if (mainMenuManager != null)
            mainMenuManager.SetMainState(MainMenuState.GalleryImage);
        panelListButtonGallery.SetActive(false);
        panelGalleryImage.SetActive(true);
        imgGallery.sprite = listButtonGallery[index].sprImageGallery;
        btnFirstGalleryImage.Select();
    }
    public void CloseGalleryImage()
    {
        panelListButtonGallery.SetActive(true);
        panelGalleryImage.SetActive(false);
    }

    public void LoadGameData(PlayerData dataPlayer, GameDataGallery dataGallery)
    {
        galleryData = dataGallery;
        SetButtonGallery();
    }

    public void SaveGameData(ref PlayerData data, ref GameDataGallery dataGallery)
    {
        dataGallery = galleryData;
    }
    #endregion
}
