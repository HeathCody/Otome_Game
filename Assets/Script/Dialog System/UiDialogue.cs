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
    [SerializeField] private TextMeshProUGUI txtCharTalkName;
    [SerializeField] private TextMeshProUGUI txtDialogue;
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
                        }
                    }
                }
            }
        }
        txtCharTalkName.text = data.CharName;
        txtDialogue.text = data.Dialogue;
        panelContent.SetActive(true);
        panelDialogue.SetActive(true);
    }
}
