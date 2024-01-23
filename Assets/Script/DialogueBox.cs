using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DialogueBox : MonoBehaviour
{
    //*****
    // Singleton pattern
    //*****
    private static DialogueBox _instance;
    public static DialogueBox Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    //*****
    // Singleton pattern
    //*****
    [HideInInspector]
    public DialogueScriptableObject currentDialogue;
    [SerializeField]
    public TMP_Text textBox;
    public TMP_Text textName;
    public TMP_Text playerName;

    public GameObject choicesParent;
    public Button[] buttons;
    private int idTextList;
    public Image talker1;
    public Image talker2;
    public GameObject playerBg;
    public GameObject interBg;

    private Color32 whiteColor = new Color32(255, 255, 255, 255);
    private Color32 greyColor = new Color32(155, 155, 155, 255);
    private Vector2 sizeTalking = new Vector2(1f, 1f);
    private Vector2 sizeNotTalking = new Vector2(0.9f, 0.9f);

    public void setOriginalText()
    {
        idTextList = 0;
        textBox.text = currentDialogue.dialogueList[0].text;
        ChangeColor();
        ChangeName();
    }

    public void nextDialogue()
    {
        if (currentDialogue.dialogueList[idTextList].idGoto != -1)
        {
            if (!currentDialogue.dialogueList[idTextList].isChoiceDialogue)
            {
                GoToNextDialogue(currentDialogue.dialogueList[idTextList].idGoto);
                SetCurrentDialogue();
            }
        }
        else
        {
            GameStateBehaviour.Instance.ChangeToMainGame();
        }
    }

    public void GoToNextDialogue(int id)
    {
        for (int i = 0; i < currentDialogue.dialogueList.Count; i++)
        {
            if (currentDialogue.dialogueList[i].idDialogue == id)
            {
                idTextList = i;
            }
        }
    }

    public void activeButtons()
    {
        choicesParent.SetActive(true);
        for (int i = 0; i < currentDialogue.dialogueList[idTextList].choices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.dialogueList[idTextList].choices[i].textChoice;
            buttons[i].GetComponentInChildren<ButtonDialogueAction>().goToID = currentDialogue.dialogueList[idTextList].choices[i].idNext;
            if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.RunMiniGame)
            {
                buttons[i].onClick.AddListener(GameStateBehaviour.Instance.ChangeToRunMiniGame);
                buttons[i].onClick.AddListener(desactivateButtons);
            }
            else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.SearchMiniGame)
            {
                buttons[i].onClick.AddListener(GameStateBehaviour.Instance.ChangeToSearchMiniGame);
                buttons[i].onClick.AddListener(desactivateButtons);
            }
            else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.GunMiniGame)
            {
                buttons[i].onClick.AddListener(GameStateBehaviour.Instance.ChangeToGunMiniGame);
                buttons[i].onClick.AddListener(desactivateButtons);
            }
        }
    }
    public void desactivateButtons()
    {
        choicesParent.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveListener(GameStateBehaviour.Instance.ChangeToRunMiniGame);
            buttons[i].onClick.RemoveListener(GameStateBehaviour.Instance.ChangeToSearchMiniGame);
            buttons[i].onClick.RemoveListener(GameStateBehaviour.Instance.ChangeToGunMiniGame);
            buttons[i].onClick.RemoveListener(desactivateButtons);
            buttons[i].gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SetCurrentDialogue()
    {
        desactivateButtons();
        ChangeName();
        textBox.text = currentDialogue.dialogueList[idTextList].text;
        ChangeColor();
        if (currentDialogue.dialogueList[idTextList].isChoiceDialogue)
        {
            activeButtons();
        }
    }

    private void ChangeColor()
    {
        if (currentDialogue.talker1 == currentDialogue.dialogueList[idTextList].charTalking)
        {
            talker1.rectTransform.localScale = sizeTalking;
            talker2.rectTransform.localScale = sizeNotTalking;

            talker1.color = whiteColor;
            talker2.color = greyColor;
            playerBg.SetActive(true);
            playerName.gameObject.SetActive(true);
            interBg.SetActive(false);
            textName.gameObject.SetActive(false);
        }
        else
        {
            talker1.rectTransform.localScale = sizeNotTalking;
            talker2.rectTransform.localScale = sizeTalking;

            talker1.color = greyColor;
            talker2.color = whiteColor;

            playerBg.SetActive(false);
            playerName.gameObject.SetActive(false);
            interBg.SetActive(true);
            textName.gameObject.SetActive(true);
        }
    }
    private void ChangeName()
    {
        textName.text = currentDialogue.dialogueList[idTextList].charTalking.ToString();
    }
}
