using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG;
using DG.Tweening;

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
    //[HideInInspector]
    public DialogueInteractionBehaviour toInteract;
    [HideInInspector]
    public SearchObjectInteraction searchObject;
    public SearchObjectInteraction takenObject;
    public Transform parentEmplacement;
    [SerializeField]
    private TMP_Text textSearch;
    [SerializeField]
    private GunBehaviour gunBehaviour;
    public bool canShoot = true;

    [HideInInspector]
    public Vector2 nextInput;

    private float nbTourPerSec;
    private float tourParMinute;
    public GameObject SpoonAnchor;
    public TMP_Text textRPM;
    public TMP_Text textRPMObj;
    public int actualObjectiveRPM = -1;
    public float[] objectivesRPM;
    public DialogueInteractionBehaviour zilyDialogue;

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


    [SerializeField]
    public Animator animator;

    [SerializeField]
    public Image searchImage;
    private Color colorTransparent = new Color(255, 255, 255, 0f);
    private Color colorNotTransparent = new Color(255, 255, 255, 255);

    [SerializeField]
    public AudioClip[] sfx;
    private AudioSource source;

    private Vector3 rotationSpoonBas;
    [SerializeField]
    private Vector3 positionSpoonBas;
    [SerializeField]
    private Vector3 rotationSpoonDroite;
    [SerializeField]
    private Vector3 positionSpoonDroite;
    [SerializeField]
    private Vector3 rotationSpoonGauche;
    [SerializeField]
    private Vector3 positionSpoonGauche;
    [SerializeField]
    private Vector3 rotationSpoonHaut;
    [SerializeField]
    private Vector3 positionSpoonHaut;
    [SerializeField]
    private Image keyToMove;
    [SerializeField]
    private Sprite keyToMoveLeft;
    [SerializeField]
    private Sprite keyToMoveRight;
    [SerializeField]
    private Sprite keyToMoveUp;
    [SerializeField]
    private Sprite keyToMoveDown;
    [SerializeField]
    private GameObject forwardChaudron;
    [SerializeField]
    private GameObject backChaudron;
    [SerializeField]
    private GameObject sauce;
    [SerializeField]
    public FactoryBehaviour factoryBehaviour;
    [SerializeField]
    private ChronoBehaviour1 chrono;

    void Awake()
    {
        source = GetComponent<AudioSource>();

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

            source.clip = sfx[1];
            source.Play();
        }
        else
        {
            GameStateBehaviour.Instance.UnpauseGame();

            source.clip = sfx[2];
            source.Play();
        }
    }

    Vector2 oldPos;


    private void Interaction(InputAction.CallbackContext obj)
    {
        GameStateBehaviour.Instance.ChangeController(obj.control.device);
        if (canInteract && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.RunMiniGame && !GameStateBehaviour.Instance.isPaused)
        {
            if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.Dialogue)
            {
                DialogueBox.Instance.nextDialogue();

                source.clip = sfx[0];
                source.Play();
            }
            else if (toInteract != null)
            {
                source.clip = sfx[3];
                source.Play();

                GameStateBehaviour.Instance.ChangeToDialogue();
                if (takenObject != null)
                {

                    if (takenObject.type == ObjectType.Papate)
                    {
                        DialogueBox.Instance.currentDialogue = toInteract.WinDialogue;
                        textSearch.text = "Objets possibles";
                        GameStateBehaviour.Instance.ChangeFarfolleDialogue(4);
                        takenObject = null;
                        //searchImage.sprite = null;
                        //searchImage.color = colorTransparent;
                    }
                    else
                    {
                        if (takenObject.type != ObjectType.Yette)
                        {
                            takenObject.found.gameObject.transform.parent.transform.position = oldPos;
                            takenObject.found.gameObject.transform.parent.transform.localScale /= 2;
                        }
                        else takenObject.found.gameObject.transform.parent.transform.localScale /= 3;

                        DialogueBox.Instance.currentDialogue = takenObject.dialogueInteraction;
                        takenObject.transform.parent.gameObject.SetActive(true);
                        takenObject.keyInteraction.SetActive(false);
                        textSearch.text = "Objets possibles";
                        takenObject = null;
                        //searchImage.sprite = null;
                        //searchImage.color = colorTransparent;
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
                        5 = InfoDialogue
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
                            DialogueBox.Instance.currentDialogue = toInteract.InfoDialogue;
                            break;
                    }
                }
                DialogueBox.Instance.setOriginalText();
                rb.velocity = Vector2.zero;
                print(GameStateBehaviour.Instance.currentState);
            }
            else if (searchObject != null)
            {
                if (takenObject == null)
                {

                    takenObject = searchObject;
                    takenObject.transform.parent.gameObject.SetActive(false);
                    textSearch.text = "En main : " + takenObject.transform.parent.name;
                    GameStateBehaviour.Instance.farfolleInteraction.SetActive(true);
                    //searchImage.sprite = takenObject.transform.parent.GetComponent<SpriteRenderer>().sprite;
                    takenObject.shadow.gameObject.SetActive(false);

                    takenObject.found.gameObject.SetActive(true);
                    if (takenObject.type != ObjectType.Yette)
                    {
                        oldPos = takenObject.found.gameObject.transform.parent.transform.position;

                        takenObject.found.gameObject.transform.parent.transform.position = new Vector2(67, 107);
                        takenObject.found.gameObject.transform.parent.transform.localScale *= 2;
                    }
                    else takenObject.found.gameObject.transform.parent.transform.localScale *= 3;

                    /*switch (takenObject.type)
                    {
                        case ObjectType.Yette:
                            takenObject.shadow.enabled = false;
                            break;
                        case ObjectType.Herbe:
                            break;
                        case ObjectType.Moulin:
                            break;
                        case ObjectType.Papate:
                            break;
                    }*/
                    //searchImage.color = colorNotTransparent;
                }

            }
        }
        else if (canShoot && GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.GunMiniGame)
        {
            gunBehaviour.GunAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(rb.velocity, Vector2.zero) < 0.5f)
        {
            animator.SetTrigger("PlayerIdle");
        }
        else if (Vector2.Distance(rb.velocity, Vector2.zero) > 0.5f)
        {
            animator.SetTrigger("PlayerWalking");
        }
        if (moveAction.ReadValue<Vector2>() != Vector2.zero)
        {
            GameStateBehaviour.Instance.ChangeController(moveAction.activeControl.device);
        }

        if (GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.Dialogue && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.GunMiniGame && GameStateBehaviour.Instance.currentState != GameStateBehaviour.GameState.FactoryMiniGame)
        {
            if (!GameStateBehaviour.Instance.isPaused && GameStateBehaviour.Instance.canMove)
            {
                Vector2 moveDir = moveAction.ReadValue<Vector2>();
                Vector2 velocity = rb.velocity;
                velocity.x = speed * moveDir.x;
                velocity.y = speed * moveDir.y;
                rb.velocity = velocity;
            }
        }
        else if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.Dialogue)
        {
            if (moveAction.ReadValue<Vector2>().x != 0 || moveAction.ReadValue<Vector2>().y != 0)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    if (DialogueBox.Instance.currentDialogue.name.Equals("AccusationWithInfoYette") || DialogueBox.Instance.currentDialogue.name.Equals("AccusationWithoutInfo"))
                    {
                        DialogueBox.Instance.buttonsFinal[0].Select();
                    }
                    else
                    {
                        if (DialogueBox.Instance.buttons[0].gameObject.activeSelf)
                        {
                            DialogueBox.Instance.buttons[0].Select();
                        }
                        else
                        {
                            DialogueBox.Instance.buttons[1].Select();
                        }
                    }
                }
            }
        }
        else if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.FactoryMiniGame)
        {
            if (factoryBehaviour.moveRightCauldron && moveAction.ReadValue<Vector2>() == Vector2.right)
            {
                factoryBehaviour.MoveRightCauldrons();
            }
            if (factoryBehaviour.moveDownCauldron && moveAction.ReadValue<Vector2>() == Vector2.down)
            {
                factoryBehaviour.MoveDownCauldrons();
            }
            if (factoryBehaviour.moveLeftCauldron && moveAction.ReadValue<Vector2>() == Vector2.left)
            {
                factoryBehaviour.MoveLeftCauldrons();
            }
            if (factoryBehaviour.moveUpCauldron && moveAction.ReadValue<Vector2>() == Vector2.up)
            {
                factoryBehaviour.MoveUpCauldrons();
            }
        }
    }

    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.FactoryMiniGame)
        {
            thingToDo();
        }
    }



    /*public void ChangeObjective()
    {
        chrono.timeTotal = 6.5f;
        actualObjectiveRPM++;
        if (actualObjectiveRPM == objectivesRPM.Length || (actualObjectiveRPM != 0 && tourParMinute < objectivesRPM[actualObjectiveRPM]))
        {
            GameStateBehaviour.Instance.ChangeToDialogue();
            DialogueBox.Instance.currentDialogue = zilyDialogue.LoseDialogue;
            GameStateBehaviour.Instance.ChangeZilyDialogue(3);
            toInteract = GameStateBehaviour.Instance.zilyInteraction.GetComponent<DialogueInteractionBehaviour>();
            DialogueBox.Instance.setOriginalText();
        }
        textRPMObj.SetText("Quota : " + objectivesRPM[actualObjectiveRPM]);
        if (actualObjectiveRPM == 1)
        {
            forwardChaudron.transform.DOShakePosition(6f, 1, 10);
            forwardChaudron.transform.DOShakeRotation(6f, 1, 10);
            backChaudron.transform.DOShakePosition(6f, 1, 10);
            backChaudron.transform.DOShakeRotation(6f, 1, 10);
            sauce.transform.DOShakePosition(6f, 1, 10);
            sauce.transform.DOShakeRotation(6f, 1, 10);
            sauce.GetComponent<Image>().DOColor(color1, 6);
        }
        else if (actualObjectiveRPM == 2)
        {
            forwardChaudron.transform.DOShakePosition(6f, 10, 10);
            forwardChaudron.transform.DOShakeRotation(6f, 10, 10);
            backChaudron.transform.DOShakePosition(6f, 10, 10);
            backChaudron.transform.DOShakeRotation(6f, 10, 10);
            sauce.transform.DOShakePosition(6f, 10, 10);
            sauce.transform.DOShakeRotation(6f, 10, 10);
            sauce.GetComponent<Image>().DOColor(color2, 6);
        }
        else if (actualObjectiveRPM == 3)
        {
            forwardChaudron.transform.DOShakePosition(6f, 15, 10);
            forwardChaudron.transform.DOShakeRotation(6f, 15, 10);
            backChaudron.transform.DOShakePosition(6f, 15, 10);
            backChaudron.transform.DOShakeRotation(6f, 15, 10);
            sauce.transform.DOShakePosition(6f, 15, 10);
            sauce.transform.DOShakeRotation(6f, 15, 10);
            sauce.GetComponent<Image>().DOColor(color3, 6);
        }
        else if (actualObjectiveRPM == 4)
        {
            forwardChaudron.transform.DOShakePosition(6f, 30, 10);
            forwardChaudron.transform.DOShakeRotation(6f, 30, 10);
            backChaudron.transform.DOShakePosition(6f, 30, 10);
            backChaudron.transform.DOShakeRotation(6f, 30, 10);
            sauce.transform.DOShakePosition(6f, 30, 10);
            sauce.transform.DOShakeRotation(6f, 30, 10);
            sauce.GetComponent<Image>().DOColor(color4, 6);
        }
        else if (actualObjectiveRPM == 5)
        {
            forwardChaudron.transform.DOShakePosition(6f, 60, 10);
            forwardChaudron.transform.DOShakeRotation(6f, 60, 10);
            backChaudron.transform.DOShakePosition(6f, 60, 10);
            backChaudron.transform.DOShakeRotation(6f, 60, 10);
            sauce.transform.DOShakePosition(6f, 60, 10);
            sauce.transform.DOShakeRotation(6f, 60, 10);
            sauce.GetComponent<Image>().DOColor(color5, 6);
        }

        StartCoroutine(DoAfterDelay(6.5f, ChangeObjective));
    }*/

}
