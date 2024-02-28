using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Reflection;
public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] voices;
    private AudioSource source;

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
    [SerializeField]
    public DialogueScriptableObject currentDialogue;
    [SerializeField]
    public TMP_Text textBox;
    public TMP_Text textName;
    public TMP_Text playerName;

    public GameObject choicesParent;
    public Button[] buttons;
    public GameObject choicesParentFinal;
    public Button[] buttonsFinal;
    public Button buttonsFinalYette;
    public Button buttonsFinalRaVito;
    private int idTextList;
    public Image talker1;
    public Image talker2;
    public GameObject playerBg;
    public GameObject interBg;
    public Animator interAnimator;

    private Color32 whiteColor = new Color32(255, 255, 255, 255);
    private Color32 greyColor = new Color32(155, 155, 155, 255);
    private Color32 absentColor = new Color32(255, 255, 255, 0);
    private Vector2 sizeTalking = new Vector2(1f, 1f);
    private Vector2 sizeNotTalking = new Vector2(0.9f, 0.9f);

    private List<Action> actions = new List<Action>();


    public void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void setOriginalText()
    {
        desactivateButtons();
        idTextList = 0;
        textBox.text = currentDialogue.dialogueList[0].text;
        talker1.sprite = GameStateBehaviour.Instance.player.spriteNeutral;
        if (GameStateBehaviour.Instance.player.toInteract != null)
        {
            interAnimator.enabled = false;
            talker2.sprite = GameStateBehaviour.Instance.player.toInteract.spriteNeutral;
            //talker2.rectTransform.sizeDelta = GameStateBehaviour.Instance.player.toInteract.dimensionImage;
        }

        ChangeColor();
        ChangeName();

        if (currentDialogue.dialogueList[0].isChoiceDialogue)
        {
            activeButtons();
        }

        if (currentDialogue.dialogueAction == ActionChoice.RunMiniGame)
        {
            actions.Add(GameStateBehaviour.Instance.ChangeToRunMiniGame);
        }
        else if (currentDialogue.dialogueAction == ActionChoice.SearchMiniGame)
        {
            actions.Add(GameStateBehaviour.Instance.ChangeToSearchMiniGame);
        }
        else if (currentDialogue.dialogueAction == ActionChoice.GunMiniGame)
        {
            actions.Add(GameStateBehaviour.Instance.ChangeToGunMiniGame);
        }
        else if (currentDialogue.dialogueAction == ActionChoice.FactoryMiniGame)
        {
            actions.Add(GameStateBehaviour.Instance.ChangeToFactoryMiniGame);
        }
        else if (currentDialogue.dialogueAction == ActionChoice.CheckYetteDialogueInfoZily)
        {
            actions.Add(GameStateBehaviour.Instance.CheckYetteDialogueInfoZily);
        }
        else if (currentDialogue.dialogueAction == ActionChoice.CheckYetteDialogueInfoRaVito)
        {
            actions.Add(GameStateBehaviour.Instance.CheckYetteDialogueInfoRaVito);
        }
        else if (currentDialogue.dialogueAction == ActionChoice.CheckYetteDialogueInfoFarfolle)
        {
            actions.Add(GameStateBehaviour.Instance.CheckYetteDialogueInfoFarfolle);
        }
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
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].GetMethodInfo().Name.Contains("CheckYetteDialogueInfo") && PlayerPrefs.GetInt(actions[i].GetMethodInfo().Name) == 0)
                {
                    actions[i].Invoke();
                    PlayerPrefs.SetInt(actions[i].GetMethodInfo().Name, 1);
                }
                else if (!actions[i].GetMethodInfo().Name.Contains("CheckYetteDialogueInfo"))
                {
                    print(actions[i].GetMethodInfo().Name);
                    actions[i].Invoke();
                }
            }
            if (currentDialogue.unlockDialogue != null)
            {
                for (int i = 0; i < currentDialogue.unlockCarnets.Length; i++)
                {
                    PlayerPrefs.SetInt(currentDialogue.unlockCarnets[i].alibiToUnlock.ToString(), 1);
                }
            }

            for (int i = 0; i < currentDialogue.unlockDialogue.Length; i++)
            {
                if (PlayerPrefs.GetInt(currentDialogue.unlockDialogue[i].characterToUnlock.ToString() + currentDialogue.name) == 0)
                {
                    GameStateBehaviour.Instance.ChangeCharDialogue(currentDialogue.unlockDialogue[i].characterToUnlock, currentDialogue.unlockDialogue[i].dialogueIdUnlock);
                    PlayerPrefs.SetInt(currentDialogue.unlockDialogue[i].characterToUnlock.ToString() + currentDialogue.name, 1);
                }
            }
            actions = new List<Action>();
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
        if (currentDialogue.name.Equals("AccusationWithInfoYette") || currentDialogue.name.Equals("AccusationWithoutInfo"))
        {
            choicesParentFinal.SetActive(true);
            for (int i = 0; i < currentDialogue.dialogueList[idTextList].choices.Count; i++)
            {
                buttonsFinal[i].GetComponentInChildren<ButtonDialogueAction>().goToID = currentDialogue.dialogueList[idTextList].choices[i].idNext;
                buttonsFinal[i].gameObject.SetActive(true);
            }

            if (currentDialogue.name.Equals("AccusationWithInfoYette"))
            {
                buttonsFinalYette.onClick.AddListener(AddActionEndingYette);
                buttonsFinalYette.onClick.AddListener(desactivateButtons);
            }
            else if (currentDialogue.name.Equals("AccusationWithoutInfo"))
            {
                buttonsFinalRaVito.onClick.AddListener(AddActionEndingRaVito);
                buttonsFinalRaVito.onClick.AddListener(desactivateButtons);
            }
        }
        else
        {
            choicesParent.SetActive(true);
            for (int i = 0; i < currentDialogue.dialogueList[idTextList].choices.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.dialogueList[idTextList].choices[i].textChoice;
                buttons[i].GetComponentInChildren<ButtonDialogueAction>().goToID = currentDialogue.dialogueList[idTextList].choices[i].idNext;


                if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.RunMiniGame)
                {
                    buttons[i].onClick.AddListener(AddActionRun);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.ChangeToRunMiniGame);
                }
                else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.SearchMiniGame || currentDialogue.dialogueAction == ActionChoice.SearchMiniGame)
                {
                    buttons[i].onClick.AddListener(AddActionSearch);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.ChangeToSearchMiniGame);
                }
                else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.GunMiniGame)
                {
                    buttons[i].onClick.AddListener(AddActionGun);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.ChangeToGunMiniGame);
                }
                else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.FactoryMiniGame)
                {
                    buttons[i].onClick.AddListener(AddActionFactory);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.ChangeToFactoryMiniGame);
                }
                else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.WinRiddleGame)
                {
                    buttons[i].onClick.AddListener(AddActionWinRiddle);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.WinRiddleGame);
                }
                else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.DialogueYetteRaVito)
                {
                    buttons[i].onClick.AddListener(AddActionYetteRaVito);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.DialogueYetteRaVito);
                }
                else if (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.DialogueYetteOmbre)
                {
                    buttons[i].onClick.AddListener(AddActionYetteOmbre);
                    buttons[i].onClick.AddListener(desactivateButtons);
                    //actions.Add(GameStateBehaviour.Instance.DialogueYetteOmbre);
                }


                if ((currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.DialogueYetteOmbre && PlayerPrefs.GetInt("YetteInfoDialogueDoneOmbre") == 1) || (currentDialogue.dialogueList[idTextList].choices[i].method == ActionChoice.DialogueYetteRaVito && PlayerPrefs.GetInt("YetteInfoDialogueDoneRaVito") == 1))
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void desactivateButtons()
    {
        choicesParent.SetActive(false);
        choicesParentFinal.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveListener(AddActionRun);
            buttons[i].onClick.RemoveListener(AddActionSearch);
            buttons[i].onClick.RemoveListener(AddActionGun);
            buttons[i].onClick.RemoveListener(AddActionFactory);
            buttons[i].onClick.RemoveListener(AddActionWinRiddle);
            buttons[i].onClick.RemoveListener(AddActionYetteRaVito);
            buttons[i].onClick.RemoveListener(AddActionYetteOmbre);
            buttons[i].onClick.RemoveListener(AddActionEndingYette);
            buttons[i].onClick.RemoveListener(AddActionEndingRaVito);
            buttons[i].onClick.RemoveListener(desactivateButtons);
            buttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < buttonsFinal.Length; i++)
        {
            buttonsFinal[i].onClick.RemoveListener(AddActionRun);
            buttonsFinal[i].onClick.RemoveListener(AddActionSearch);
            buttonsFinal[i].onClick.RemoveListener(AddActionGun);
            buttonsFinal[i].onClick.RemoveListener(AddActionFactory);
            buttonsFinal[i].onClick.RemoveListener(AddActionWinRiddle);
            buttonsFinal[i].onClick.RemoveListener(AddActionYetteRaVito);
            buttonsFinal[i].onClick.RemoveListener(AddActionYetteOmbre);
            buttonsFinal[i].onClick.RemoveListener(AddActionEndingYette);
            buttonsFinal[i].onClick.RemoveListener(AddActionEndingRaVito);
            buttonsFinal[i].onClick.RemoveListener(desactivateButtons);
            buttonsFinal[i].gameObject.SetActive(false);
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
            talker1.rectTransform.localScale = new Vector2(1f, 1f);
            talker2.rectTransform.localScale = new Vector2(0.9f, 0.9f);

            talker1.color = whiteColor;
            talker2.color = greyColor;
            playerBg.SetActive(true);
            playerName.gameObject.SetActive(true);
            interBg.SetActive(false);
            textName.gameObject.SetActive(false);
            Talker1Sprite();
        }
        else
        {
            talker1.rectTransform.localScale = new Vector2(0.9f, 0.9f);
            talker2.rectTransform.localScale = Vector2.one;

            talker1.color = greyColor;
            talker2.color = whiteColor;

            playerBg.SetActive(false);
            playerName.gameObject.SetActive(false);
            interBg.SetActive(true);
            textName.gameObject.SetActive(true);
            Talker2Sprite();
            /*if (currentDialogue.dialogueList[idTextList].charTalking == Character.Maili_Mailo)
            {
                interAnimator.SetTrigger("MailiMailoIsTalking");
                interAnimator.enabled = true;
            }*/
            if (currentDialogue.dialogueList[idTextList].charTalking == Character.Ra_Vito && PlayerPrefs.GetInt("RaVitoKetchup") == 1)
            {
                interAnimator.SetTrigger("Ra_Vito_KetchupedIsTalking");
            }
            else
            {
                interAnimator.SetTrigger(currentDialogue.talker2.ToString() + "IsTalking");
            }
            interAnimator.enabled = true;


            //sons
            if (source != null)
            {
                if (currentDialogue.talker2.ToString() == "Maili_Mailo")
                {
                    source.clip = voices[0];
                    source.Play();
                }
                else if (currentDialogue.talker2.ToString() == "Yette")
                {
                    source.clip = voices[1];
                    source.Play();
                }
                else if (currentDialogue.talker2.ToString() == "Ghetti")
                {
                    source.clip = voices[2];
                    source.Play();
                }
                else if (currentDialogue.talker2.ToString() == "Farfolle")
                {
                    source.clip = voices[3];
                    source.Play();
                }
                else if (currentDialogue.talker2.ToString() == "Zily")
                {
                    source.clip = voices[4];
                    source.Play();
                }
                else if (currentDialogue.talker2.ToString() == "Ra_Vito")
                {
                    if (UnityEngine.Random.Range(1, 11) == 10)
                    {
                        source.clip = voices[6];
                    }
                    else source.clip = voices[5];
                    source.Play();
                }
            }
            talker2.rectTransform.anchorMin = Vector2.zero;
            talker2.rectTransform.anchorMax = Vector2.one;
            talker2.rectTransform.offsetMin = new Vector2(0, 0);
            talker2.rectTransform.offsetMin = new Vector2(0, 0);
        }
    }
    private void ChangeName()
    {
        textName.text = currentDialogue.dialogueList[idTextList].charTalking.ToString().Replace("_", " ");
    }

    public void AddActionRun()
    {
        actions.Add(GameStateBehaviour.Instance.ChangeToRunMiniGame);
    }
    public void AddActionSearch()
    {
        actions.Add(GameStateBehaviour.Instance.ChangeToSearchMiniGame);
    }
    public void AddActionGun()
    {
        actions.Add(GameStateBehaviour.Instance.ChangeToGunMiniGame);
    }
    public void AddActionFactory()
    {
        actions.Add(GameStateBehaviour.Instance.ChangeToFactoryMiniGame);
    }
    public void AddActionWinRiddle()
    {
        actions.Add(GameStateBehaviour.Instance.WinRiddleGame);
    }
    public void AddActionYetteRaVito()
    {
        actions.Add(GameStateBehaviour.Instance.DialogueYetteRaVito);
    }
    public void AddActionYetteOmbre()
    {
        actions.Add(GameStateBehaviour.Instance.DialogueYetteOmbre);
    }
    public void AddActionEndingYette()
    {
        actions.Add(GameStateBehaviour.Instance.EndingYette);
    }
    public void AddActionEndingRaVito()
    {
        actions.Add(GameStateBehaviour.Instance.EndingRaVito);
    }

    public void Talker1Sprite()
    {
        CharacterBehaviour toInteract = GameStateBehaviour.Instance.player;
        if (PlayerPrefs.GetInt("PlayerKetchup") == 1)
        {
            if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Neutral)
            {
                talker1.sprite = toInteract.spriteNeutralKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Questioned)
            {
                talker1.sprite = toInteract.spriteQuestionedKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Ashamed)
            {
                talker1.sprite = toInteract.spriteAshamedKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Shocked)
            {
                talker1.sprite = toInteract.spriteShockedKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Sad)
            {
                talker1.sprite = toInteract.spriteSadKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Happy)
            {
                talker1.sprite = toInteract.spriteHappyKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Angry)
            {
                talker1.sprite = toInteract.spriteAngryKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Absent)
            {
                talker1.sprite = toInteract.spriteNeutralKetchuped;
                talker1.color = absentColor;
            }
        }
        else
        {
            if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Neutral)
            {
                talker1.sprite = toInteract.spriteNeutral;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Questioned)
            {
                talker1.sprite = toInteract.spriteQuestioned;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Ashamed)
            {
                talker1.sprite = toInteract.spriteAshamed;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Shocked)
            {
                talker1.sprite = toInteract.spriteShocked;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Sad)
            {
                talker1.sprite = toInteract.spriteSad;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Happy)
            {
                talker1.sprite = toInteract.spriteHappy;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Angry)
            {
                talker1.sprite = toInteract.spriteAngry;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Absent)
            {
                talker1.sprite = toInteract.spriteNeutral;
                talker1.color = absentColor;
            }
        }

    }
    public void Talker2Sprite()
    {
        if (source != null)
        {
            source.Stop();
        }
        DialogueInteractionBehaviour toInteract = GameStateBehaviour.Instance.player.toInteract;
        if (PlayerPrefs.GetInt("RaVitoKetchup") == 1 && toInteract.transform.parent.name.Equals("Ra_Vito"))
        {
            if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Neutral)
            {
                talker2.sprite = toInteract.spriteNeutralKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Questioned)
            {
                talker2.sprite = toInteract.spriteQuestionedKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Ashamed)
            {
                talker2.sprite = toInteract.spriteAshamedKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Shocked)
            {
                talker2.sprite = toInteract.spriteShockedKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Sad)
            {
                talker2.sprite = toInteract.spriteSadKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Happy)
            {
                talker2.sprite = toInteract.spriteHappyKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Angry)
            {
                talker2.sprite = toInteract.spriteAngryKetchuped;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Absent)
            {
                talker2.sprite = toInteract.spriteNeutralKetchuped;
                talker2.color = absentColor;
            }
        }
        else
        {
            if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Neutral)
            {
                talker2.sprite = toInteract.spriteNeutral;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Questioned)
            {
                talker2.sprite = toInteract.spriteQuestioned;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Ashamed)
            {
                talker2.sprite = toInteract.spriteAshamed;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Shocked)
            {
                talker2.sprite = toInteract.spriteShocked;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Sad)
            {
                talker2.sprite = toInteract.spriteSad;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Happy)
            {
                talker2.sprite = toInteract.spriteHappy;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Angry)
            {
                talker2.sprite = toInteract.spriteAngry;
            }
            else if (currentDialogue.dialogueList[idTextList].emotion == Emotions.Absent)
            {
                talker2.sprite = toInteract.spriteNeutral;
                talker2.color = absentColor;
            }
        }
    }
}
