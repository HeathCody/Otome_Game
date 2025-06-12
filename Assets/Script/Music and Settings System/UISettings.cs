using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public GameObject panelSettings;
    public Slider textSpeedSld;
    public Slider AutoTextSpeedSld;
    public Slider musicVolumeSld;
    public Slider soundVolumeSld;

    public void CloseSettings()
    {
        panelSettings.SetActive(false);
    }
    public void OpenSettings()
    {
        panelSettings.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


}
