using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChoice : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private GameObject panelContent;
    [SerializeField] private GameObject prefabButtonChoice;
    [SerializeField] private Transform transListButtonChoice;
    [SerializeField] private List<GameObject> listCurrentButton;
    ChoiceSo currentChoice;
    private void Start()
    {
        CloseChoice();
    }
    public void OpenChoice(ChoiceSo choice)
    {
        for (int i = 0; i < listCurrentButton.Count; i++) listCurrentButton[i].SetActive(false);
        currentChoice = choice;
        if (listCurrentButton.Count < choice.listChoice.Count)
        {
            for (int i = listCurrentButton.Count; i < currentChoice.listChoice.Count; i++)
            {
                GameObject prefab = Instantiate(prefabButtonChoice, transListButtonChoice);
                int index = i;
                prefab.GetComponent<Button>().onClick.AddListener(() => ButtonChoice(index));
                prefab.gameObject.SetActive(false);
                listCurrentButton.Add(prefab);
            }
        }
        for (int i = 0; i < currentChoice.listChoice.Count; i++)
        {
            listCurrentButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentChoice.listChoice[i].ChoiceTittle;
            listCurrentButton[i].SetActive(true);
        }
        panelContent.SetActive(true);
    }
    private void CloseChoice()
    {
        panelContent.SetActive(false);
    }
    public void ButtonChoice(int index)
    {
        gameplayManager.AddReputation(currentChoice.listChoice[index].listReputation);
        switch (currentChoice.listChoice[index].eventSelectChoice)
        {
            case EventGame.OpenDIalogue:
                CloseChoice();
                gameplayManager.OpenConversation(currentChoice.listChoice[index].nextConversation);
                break;
            case EventGame.OpenChoice:
                gameplayManager.OpenChoice(currentChoice.listChoice[index].openChoice);
                break;
            case EventGame.OpenCinematic:
                CloseChoice();
                gameplayManager.OpenCinematic(currentChoice.listChoice[index].sprCinematic);
                break;
            case EventGame.OpenMap:
                CloseChoice();
                gameplayManager.OpenMap();
                break;
            case EventGame.OpenMinigame:
                CloseChoice();
                gameplayManager.OpenMinigame();
                break;
        }
    }
}
