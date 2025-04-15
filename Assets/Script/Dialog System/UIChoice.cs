using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChoice : MonoBehaviour
{
    [SerializeField] private GameplayManager gm;
    [SerializeField] private GameObject panelContent;
    [SerializeField] private GameObject prefabButtonChoice;
    [SerializeField] private Transform transListButtonChoice;
    [SerializeField] private List<GameObject> listCurrentButton;
    ChoiceSO currentChoice;
    private void Awake()
    {
        panelContent.SetActive(false);
    }
    public void OpenChoice(ChoiceSO choice)
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
        gm.AddReputation(currentChoice.listChoice[index].listReputation);
        switch (currentChoice.listChoice[index].eventSelectChoice)
        {
            case EventGame.OpenConversation:
                CloseChoice();
                gm.OpenConversation(currentChoice.listChoice[index].conversation);
                break;
            case EventGame.OpenChoice:
                gm.OpenChoice(currentChoice.listChoice[index].choice);
                break;
            case EventGame.OpenCinematic:
                CloseChoice();
                gm.OpenCinematic(currentChoice.listChoice[index].cinematic);
                break;
            case EventGame.OpenMap:
                CloseChoice();
                gm.OpenMap();
                break;
            case EventGame.OpenMinigame:
                CloseChoice();
                gm.OpenMinigame(currentChoice.listChoice[index].minigameName);
                break;
            case EventGame.OpenNaration:
                CloseChoice();
                gm.OpenNaration(currentChoice.listChoice[index].naration);
                break;
        }
    }
}
