using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueInteractionBehaviour : MonoBehaviour
{
    [SerializeField]
    public DialogueScriptableObject WinDialogue;
    [SerializeField]
    public DialogueScriptableObject LoseDialogue;
    [SerializeField]
    public DialogueScriptableObject NotUnlockGameDialogue;
    [SerializeField]
    public DialogueScriptableObject FinishGameDialogue;
    [SerializeField]
    public DialogueScriptableObject LaunchGameDialogue;
    [SerializeField]
    public DialogueScriptableObject InfoDialogue;

    [SerializeField]
    public GameObject keyInteraction;

    [SerializeField]
    public Sprite spriteNeutral;
    [SerializeField]
    public Sprite spriteAngry;
    [SerializeField]
    public Sprite spriteSad;
    [SerializeField]
    public Sprite spriteHappy;
    [SerializeField]
    public Sprite spriteQuestioned;
    [SerializeField]
    public Sprite spriteShocked;
    [SerializeField]
    public Sprite spriteAshamed;
    [SerializeField]
    public Sprite spriteAbsent;

    [SerializeField]
    public Sprite spriteNeutralKetchuped;
    [SerializeField]
    public Sprite spriteAngryKetchuped;
    [SerializeField]
    public Sprite spriteSadKetchuped;
    [SerializeField]
    public Sprite spriteHappyKetchuped;
    [SerializeField]
    public Sprite spriteQuestionedKetchuped;
    [SerializeField]
    public Sprite spriteShockedKetchuped;
    [SerializeField]
    public Sprite spriteAshamedKetchuped;
    [SerializeField]
    public Sprite spriteAbsentKetchuped;

    public Vector2 dimensionImage;
    public Vector2 sizeNotTalking;
    public Vector2 sizeTalking;
    public GameObject exclamation;

    [Header("InputController")]
    [SerializeField]
    private GameObject inputGamepad;
    [SerializeField]
    private GameObject inputKeyboard;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.RunMiniGame)
        {
            other.GetComponent<CharacterBehaviour>().canInteract = true;
            other.GetComponent<CharacterBehaviour>().toInteract = this;
            keyInteraction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.RunMiniGame)
        {
            col.GetComponent<CharacterBehaviour>().canInteract = false;
            col.GetComponent<CharacterBehaviour>().toInteract = null;
            keyInteraction.SetActive(false);

        }
    }

    private void ChangeUIController()
    {
        if (GameStateBehaviour.Instance.currentController.name.Contains("Controller"))
        {
            inputGamepad.SetActive(true);
            inputKeyboard.SetActive(false);
        }
        else if (GameStateBehaviour.Instance.currentController.name.Contains("Keyboard"))
        {
            inputGamepad.SetActive(false);
            inputKeyboard.SetActive(true);
        }
    }
}
