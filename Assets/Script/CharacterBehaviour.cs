using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterBehaviour : MonoBehaviour
{

    private CharacterInput characterInput;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction pauseAction;
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField]
    public float speed;

    [Header("Interaction Variables")]
    [SerializeField]
    public bool canInteract;
    [HideInInspector]
    public DialogueInteractionBehaviour toInteract;
    [HideInInspector]
    public SearchObjectInteraction searchObject;
    public SearchObjectInteraction takenObject;
    public Transform parentEmplacement;
    [SerializeField]
    private TMP_Text textSearch;
    [SerializeField]
    private GunBehaviour gunBehaviour;

    void Awake()
    {
        characterInput = new CharacterInput();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction = characterInput.Player.Move;
        moveAction.Enable();
        interactAction = characterInput.Player.Interaction;
        interactAction.Enable();
        pauseAction = characterInput.Player.PauseMenu;
        pauseAction.Enable();
        interactAction.performed += Interaction;
        pauseAction.performed += PauseGame;

    }

    private void OnDisable()
    {
        moveAction.Disable();
        interactAction.Disable();
        pauseAction.Disable();
        interactAction.performed -= Interaction;
        pauseAction.performed -= PauseGame;
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        if (!GameStateBehaviour.Instance.isPaused)
        {
            GameStateBehaviour.Instance.PauseGame();
        }
        else
        {
            GameStateBehaviour.Instance.UnpauseGame();
        }
    }



    private void Interaction(InputAction.CallbackContext obj)
    {
        if (canInteract && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.RunMiniGame)
        {
            if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.Dialogue)
            {
                DialogueBox.Instance.nextDialogue();
            }
            else if (toInteract != null)
            {
                GameStateBehaviour.Instance.ChangeToDialogue();
                if (takenObject != null)
                {
                    if (takenObject.type.Equals("Papate"))
                    {
                        DialogueBox.Instance.currentDialogue = toInteract.WinDialogue;
                        textSearch.text = "";
                        GameStateBehaviour.Instance.ChangeFarfolleDialogue(4);
                        takenObject = null;
                    }
                    else
                    {
                        DialogueBox.Instance.currentDialogue = takenObject.dialogueInteraction;
                        takenObject.transform.parent.gameObject.SetActive(true);
                        takenObject.keyInteraction.SetActive(false);
                        textSearch.text = "";
                        takenObject = null;
                    }
                }
                else
                {
                    switch (PlayerPrefs.GetInt(toInteract.transform.parent.name))
                    {
                        /*
                        0 = LaunchGameDialogue
                        1 = NotUnlock
                        2 = WinDialogue
                        3 = LoseDialogue
                        4 = FinishGameDialogue
                        5 = AllMiniGameDoneDialogue
                        6 = YetteDialogue
                        */
                        case 0:
                            DialogueBox.Instance.currentDialogue = toInteract.LaunchGameDialogue;
                            break;
                        case 1:
                            DialogueBox.Instance.currentDialogue = toInteract.NotUnlockGameDialogue;
                            break;
                        case 2:
                            DialogueBox.Instance.currentDialogue = toInteract.WinDialogue;
                            break;
                        case 3:
                            DialogueBox.Instance.currentDialogue = toInteract.LoseDialogue;
                            break;
                        case 4:
                            DialogueBox.Instance.currentDialogue = toInteract.FinishGameDialogue;
                            break;
                        case 5:
                            DialogueBox.Instance.currentDialogue = toInteract.AllMiniGameDoneDialogue;
                            break;
                        case 6:
                            DialogueBox.Instance.currentDialogue = toInteract.YetteDialogue;
                            break;
                    }
                }
                DialogueBox.Instance.setOriginalText();
                rb.velocity = Vector2.zero;
            }
            else if (searchObject != null)
            {
                if (takenObject == null)
                {
                    takenObject = searchObject;
                    takenObject.transform.parent.gameObject.SetActive(false);
                    textSearch.text = takenObject.transform.parent.name;
                    GameStateBehaviour.Instance.farfolleInteraction.SetActive(true);
                }
            }
        }
        else if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.GunMiniGame)
        {
            gunBehaviour.GunAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.Dialogue && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.GunMiniGame)
        {
            if (!GameStateBehaviour.Instance.isPaused)
            {
                Vector2 moveDir = moveAction.ReadValue<Vector2>();
                Vector2 velocity = rb.velocity;
                velocity.x = speed * moveDir.x;
                velocity.y = speed * moveDir.y;
                rb.velocity = velocity;
            }
        }
        else if (GameStateBehaviour.Instance.isPaused)
        {
            Vector2 moveDir = moveAction.ReadValue<Vector2>();
        }
        else if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.Dialogue)
        {
            if (moveAction.ReadValue<Vector2>().x != 0 || moveAction.ReadValue<Vector2>().y != 0)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    DialogueBox.Instance.buttons[0].Select();
                }
            }
        }
    }
}
