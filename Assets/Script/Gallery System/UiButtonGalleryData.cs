using UnityEngine;
using UnityEngine.UI;

public class UiButtonGalleryData : MonoBehaviour
{
    [SerializeField] private UiPanelGallery UiGallery;
    public string tittle;
    [SerializeField] private int indexButton;
    [SerializeField] private Image imgButton;
    public Sprite sprImageGallery;
    [SerializeField] private Sprite sprImageGalleryLock;
    public bool isUnlock;
    public void SetButtonGallery()
    {
        if (isUnlock)
            imgButton.sprite = sprImageGallery;
        else
            imgButton.sprite = sprImageGalleryLock;
    }
    public void ButtonGallery()
    {
        if (!isUnlock) return;
        UiGallery.OpenGalleryImage(indexButton);
    }
}
