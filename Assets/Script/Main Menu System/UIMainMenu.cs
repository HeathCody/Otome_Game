using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Sprite[] imgBook;
    public Image bookPanel;
    public string Chapter = "Chapter 0";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Chapter = LoadSaveManager.instance.loadSaveChapter;
        bookChange();
    }

    public void bookChange()
    {
        switch (Chapter)
        {
            case "Chapter 0":
                bookPanel.sprite = imgBook[0];
                break;
            case "Chapter 1":
                bookPanel.sprite = imgBook[1];
                break;
        }
    }

    public void changeEmergency()
    {
        LoadSaveManager.instance.loadSaveChapter = "Chapter 1";
    }
}
