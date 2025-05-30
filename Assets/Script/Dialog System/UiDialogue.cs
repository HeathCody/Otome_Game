using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiDialogue : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject panelContent;
    [SerializeField] private GameObject panelDialogue;
    [SerializeField] private Image imgBg;
    [SerializeField] private Sprite sprBgDefault;
    [SerializeField] private List<Image> listImgCharTalk;
    [SerializeField] private List<Image> listImgCharBlack;
    [SerializeField] private Color colorOnTalk;
    [SerializeField] private Color colorNotTalk;
    [SerializeField] private GameObject panelCharBox;
    [SerializeField] private Image imgCharBox;
    [SerializeField] private TextMeshProUGUI txtCharTalkName;
    [SerializeField] public TextMeshProUGUI txtDialogue;

    //efek
    [SerializeField] public Efek efek;
    [SerializeField] private CanvasGroup canvasGroup;

    public CanvasGroup GetCanvasGroup()
    {
        return canvasGroup;
    }

    public float textSpeed = 0.05f;
    public DialogueManager dialogueManager;
    public void ResetDialogue(bool isCloseAll = true)
    {
        imgBg.sprite = isCloseAll ? sprBgDefault : imgBg.sprite;
        for (int i = 0; i < listImgCharTalk.Count; i++)
        {
            listImgCharTalk[i].gameObject.SetActive(false);
            listImgCharBlack[i].gameObject.SetActive(false);
        }
        txtCharTalkName.text = "";
        txtDialogue.text = "";
        panelContent.SetActive(isCloseAll ? false : true);
        efek.InitFadeOut(this);
        efek.FadeOut();
        panelDialogue.SetActive(false);
    }
    public void SetDialogue(DialogSO data)
    {
        if (imgBg.sprite != data.BgDialogue) imgBg.sprite = data.BgDialogue;
        for (int i = 0; i < listImgCharTalk.Count; i++)
        {
            listImgCharTalk[i].gameObject.SetActive(false);
            listImgCharBlack[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < listImgCharTalk.Count; i++)
        {
            for (int j = 0; j < data.ListCharTalk.Count; j++)
            {
                if (data.ListCharTalk[j].TalkCharPos - 1 == i)
                {
                    listImgCharTalk[i].sprite = data.ListCharTalk[j].SprTalkChar;
                    listImgCharBlack[i].sprite = data.ListCharTalk[j].SprTalkChar;
                    listImgCharBlack[i].color = data.ListCharTalk[j].IsTalk ? colorOnTalk : colorNotTalk;
                    listImgCharTalk[i].gameObject.SetActive(true);
                    listImgCharBlack[i].gameObject.SetActive(true);
                    if (data.ListCharTalk[j].IsTalk) listImgCharTalk[i].transform.SetAsLastSibling();
                    if (data.ListCharTalk[j].isAnimated)
                    {
                        switch (data.ListCharTalk[j].anim)
                        {
                            case "Jump":
                                listImgCharTalk[i].GetComponent<Animator>().SetTrigger("Jump");
                                break;
                            case "SlideDown":
                                StartCoroutine(PlayDelayedAnimation(listImgCharTalk[i].GetComponent<Animator>(), "SlideDown"));
                                //listImgCharTalk[i].GetComponent<Animator>().SetTrigger("SlideDown");
                                break;
                            case "CharShake":
                                listImgCharTalk[i].GetComponent<Animator>().SetTrigger("CharShake");
                                break;
                            case "ZoomInFast":
                                listImgCharTalk[i].GetComponent<Animator>().SetTrigger("ZoomInFast");
                                break;
                            case "ZoomOutFast":
                                listImgCharTalk[i].GetComponent<Animator>().SetTrigger("ZoomOutFast");
                                break;
                            case "ScreenShake":
                                panelContent.GetComponent<Animator>().SetTrigger("ScreenShake");
                                break;
                        }
                    }
                }
            }
        }
        txtCharTalkName.text = data.CharName;
        imgCharBox.sprite = data.SprBoxChar;
        txtCharTalkName.text = data.CharName;
        efek.InitFadeIn(this);
        efek.FadeIn();
        panelContent.SetActive(true);
        panelDialogue.SetActive(true);

        StartCoroutine(TypeLine(data));
    }

    IEnumerator PlayDelayedAnimation(Animator animator, string triggerName)
    {
        yield return new WaitForSeconds(0.05f);
        animator.SetTrigger(triggerName);
    }

    IEnumerator TypeLine(DialogSO data)
    {
        foreach (char c in data.Dialogue.ToCharArray())
        {
            txtDialogue.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Update is called once per frame
    public void OnDialogueClick()
    {
            if (txtDialogue.text == dialogueManager.currentDialogue.Dialogue)
            {
                dialogueManager.NextDialogue();
            }
            else
            {
                StopAllCoroutines();
                txtDialogue.text = dialogueManager.currentDialogue.Dialogue;
            }
    }
}
