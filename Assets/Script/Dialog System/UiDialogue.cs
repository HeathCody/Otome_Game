using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiDialogue : MonoBehaviour
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
    [SerializeField] private DialogSO currentDialogue;

    //efek
    public CanvasGroup canvasGroup;
    [SerializeField] private float durationFade = 1f;

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
        panelDialogue.SetActive(false);
    }
    public void SetDialogue(DialogSO data, EffectEvent effect)
    {
        currentDialogue = data;
        if (imgBg.sprite != currentDialogue.BgDialogue) imgBg.sprite = currentDialogue.BgDialogue;
        panelContent.SetActive(true);
        for (int i = 0; i < listImgCharTalk.Count; i++)
        {
            listImgCharTalk[i].gameObject.SetActive(false);
            listImgCharBlack[i].gameObject.SetActive(false);
        }
        txtCharTalkName.text = currentDialogue.CharName;
        imgCharBox.sprite = currentDialogue.SprBoxChar;
        panelDialogue.SetActive(true);
        switch (effect)
        {
            case EffectEvent.None:
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                StartTalking();
                break;
            case EffectEvent.FadeIn:
                EffectFadeIn();
                break;
        }
    }
    private void EffectFadeIn()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(IeFade(true));
    }
    public void EffectFadeOut()
    {
        canvasGroup.alpha = 1;
        StartCoroutine(IeFade(false));
    }
    IEnumerator IeFade(bool isFadeIn)
    {
        canvasGroup.interactable = false;
        float time = 0;
        while (time < durationFade)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, isFadeIn ? 1 : 0, time / durationFade);
            time += Time.deltaTime;
            yield return null;
        }
        if (isFadeIn)
            StartTalking();
    }
    private void StartTalking()
    {
        canvasGroup.interactable = true;
        for (int i = 0; i < listImgCharTalk.Count; i++)
        {
            for (int j = 0; j < currentDialogue.ListCharTalk.Count; j++)
            {
                if (currentDialogue.ListCharTalk[j].TalkCharPos - 1 == i)
                {
                    listImgCharTalk[i].sprite = currentDialogue.ListCharTalk[j].SprTalkChar;
                    listImgCharBlack[i].sprite = currentDialogue.ListCharTalk[j].SprTalkChar;
                    listImgCharBlack[i].color = currentDialogue.ListCharTalk[j].IsTalk ? colorOnTalk : colorNotTalk;
                    listImgCharTalk[i].gameObject.SetActive(true);
                    listImgCharBlack[i].gameObject.SetActive(true);
                    if (currentDialogue.ListCharTalk[j].IsTalk) listImgCharTalk[i].transform.SetAsLastSibling();
                    if (currentDialogue.ListCharTalk[j].isAnimated)
                    {
                        switch (currentDialogue.ListCharTalk[j].anim)
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
        StartCoroutine(TypeLine(currentDialogue));
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
