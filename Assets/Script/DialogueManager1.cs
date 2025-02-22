using UnityEngine;
using System.Threading;
using TMPro;
using UnityEngine.UI;

public class DialogueManager1 : MonoBehaviour
{
    public static DialogueManager1 Instance { get; private set; }

    //UI references
    public GameObject DialogueParent; //Main container/Panel utama for dialogue UI
    public Button NextButton; //untuk next dialog dilain response
    public TextMeshProUGUI DialogueTitleText, DialogueBodyText; //text components for title and body
    public GameObject responseButtonPrefab; //prefab untuk meng-generate response buttons
    public Transform responseButtonContainer; //untuk memasukkan posisi response buttons

    public int currentDialogueIndex = 0;
    private void Awake()
    {
        //singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    //start dialog dengan set judul dan dialog node
    public void StartDialogue(string title, DialogueNode node)
    {
        //menampilkan UI dialogue
        ShowDialogue();

        //set judul dialog dan body text
        DialogueTitleText.text = title;
        DialogueBodyText.text = node.dialogueText[0];

        //remove response buttons yang exist
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        //hapus listener yang lama dulu agar bisa diganti yang baru
        NextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        //setup button untuk trigger SelectResponse ketika ditekan
        NextButton.GetComponent<Button>().onClick.AddListener(() => NextDialogue(node, title));
    }

    //handle next dialog ketika tidak memerlukan response
    public void NextDialogue(DialogueNode node, string title)
    {
        DialogueTitleText.text = title;
        if (currentDialogueIndex < node.dialogueText.Count - 1)
        {
            currentDialogueIndex++;
            DialogueBodyText.text = node.dialogueText[currentDialogueIndex];
            Debug.Log($"Next Dialogue: {currentDialogueIndex} / {node.dialogueText.Count - 1}");
        }

        //kondisi ketika dialog butuh respons atau tidak
        if (currentDialogueIndex == node.dialogueText.Count - 1)
        {
            Debug.Log("kondisi terpenuhi");
            //membuat dan setup response buttons based on current dialogue node
            foreach (DialogueResponse response in node.responses)
            {
                GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

                //setup button untuk trigger SelectResponse ketika ditekan
                buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response, title));
            }

            NextButton.interactable = false;
        }
        else
        {
            Debug.Log("kondisi tidak terpenuhi");
            NextButton.interactable = true;

        }
    }

    //handle response selection dan trigger next dialogue node
    public void SelectResponse(DialogueResponse response, string title)
    {
        if(!response.nextNode.IsLastNode())
        {
            StartDialogue(title, response.nextNode); //start next dialogue
        }
        else
        {
            HideDialogue();
        }
    }

    public void HideDialogue()
    {
        DialogueParent.SetActive(false);
    }
    
    public void ShowDialogue()
    {
        DialogueParent.SetActive(true);
    }
}
