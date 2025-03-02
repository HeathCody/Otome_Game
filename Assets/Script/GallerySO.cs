using UnityEngine;

[CreateAssetMenu(fileName = "GallerySO", menuName = "Scriptable Objects/GallerySO")]
public class GallerySO : ScriptableObject
{
    [Tooltip("Masukkan judul Image-nya")]
    public string Judul;

    [Tooltip("Drag and Drop Image yang digunakan")]
    public Sprite SprGallery;

    [Tooltip("Menentukan apakah Image sudah terbuka")]
    public bool IsOpen;
}
