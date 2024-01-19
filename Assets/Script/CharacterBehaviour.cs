using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;

public class CharacterBehaviour : MonoBehaviour
{

    private CharacterInput characterInput;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction pauseAction;
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField]
    public float speed;

    [Header("Interaction Variables")]
    [SerializeField]
    public bool canInteract;
    [HideInInspector]
    public DialogueInteractionBehaviour toInteract;

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
        if (canInteract)
        {
            if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.Dialogue)
            {
                DialogueBox.Instance.nextDialogue();
            }
            else if (toInteract != null)
            {
                GameStateBehaviour.Instance.ChangeToDialogue();
                DialogueBox.Instance.currentDialogue = toInteract.dialogueData;
                DialogueBox.Instance.setOriginalText();
                rb.velocity = Vector2.zero;
            }
            else
            {
                print("toimplement");
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.MainGame && !GameStateBehaviour.Instance.isPaused)
        {
            Vector2 moveDir = moveAction.ReadValue<Vector2>();
            Vector2 velocity = rb.velocity;
            velocity.x = speed * moveDir.x;
            velocity.y = speed * moveDir.y;
            rb.velocity = velocity;
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
