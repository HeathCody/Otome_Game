using UnityEngine;

public class GalleryData : MonoBehaviour
{
    [Tooltip("Masukkan judul Image-nya")]
    public string Judul;

    [Tooltip("Drag and Drop Image yang digunakan")]
    public Sprite SprGallery;

    [Tooltip("Menentukan apakah Image sudah terbuka")]
    public bool IsOpen;
}
