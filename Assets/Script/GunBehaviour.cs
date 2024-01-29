using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{
    public bool playerTurn;
    public GameObject player;
    public GameObject gun;
    public Transform posGame;
    public Transform posAfterGame;
    public Camera mainCamera;
    public DialogueInteractionBehaviour dialogueInteractionBehaviour;
    public Image transitionWhite;
    public Image transitionBlack;
    private int numberOfShot;
    private int whichShot;

    [SerializeField]
    public GameObject key;

    [SerializeField]
    private Sprite playerKetchup;
    [SerializeField]
    private Sprite raVitoKetchup;
    [SerializeField]
    private bool noShoot = false;

    [SerializeField]
    public AudioClip[] sfx;
    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.clip = sfx[0];

        playerTurn = false;
        whichShot = Random.Range(0, 6);
    }

    public void GunAnimation()
    {
        key.SetActive(false);
        player.GetComponent<CharacterBehaviour>().canShoot = false;
        gun.transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.WorldAxisAdd).OnComplete(() =>
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (!noShoot)
            {
                player.GetComponent<CharacterBehaviour>().canShoot = true;
                key.SetActive(true);
            }

        });
        if (numberOfShot != whichShot)
        {
            sound.Play();

            playerTurn = !playerTurn;
            gun.GetComponent<SpriteRenderer>().flipX = !playerTurn;
            if (mainCamera.orthographicSize > 2.5f)
            {
                mainCamera.DOOrthoSize(mainCamera.orthographicSize - 0.5f, 0.1f);
            }
            else
            {
                mainCamera.DOOrthoSize(mainCamera.orthographicSize + 0.5f, 0.1f).OnComplete(() => mainCamera.DOOrthoSize(mainCamera.orthographicSize - 0.5f, 0.1f));
            }
            mainCamera.DOShakePosition(0.2f, 2, 50);
            numberOfShot++;
        }
        else
        {
            sound.clip = sfx[1];
            sound.Play();

            noShoot = true;
            numberOfShot++;
            mainCamera.DOShakePosition(0.2f, 2, 50);
            transitionWhite.gameObject.SetActive(true);
            player.GetComponent<CharacterBehaviour>().canShoot = false;
            transitionWhite.DOFade(1, 0.1f).OnComplete(() =>
            {
                if (playerTurn)
                {
                    GameStateBehaviour.Instance.playerSprite.sprite = playerKetchup;
                    PlayerPrefs.SetInt("PlayerKetchup", 1);
                }
                else
                {
                    GameStateBehaviour.Instance.raVitoSprite.sprite = raVitoKetchup;
                    PlayerPrefs.SetInt("RaVitoKetchup", 1);
                }
                player.transform.position = posAfterGame.position;
                mainCamera.orthographicSize = 5f;
                gun.SetActive(false);
                mainCamera.GetComponent<SmoothFollow>().follow = player;
                transitionWhite.DOFade(1, 1f).OnComplete(() =>
                {
                    GameStateBehaviour.Instance.pistolMiniGameScreen.SetActive(false);
                    transitionWhite.DOFade(0, 3).OnComplete(() =>
                    {
                        transitionWhite.gameObject.SetActive(false);
                        GameStateBehaviour.Instance.ChangeToDialogue();
                        if (playerTurn)
                        {
                            DialogueBox.Instance.currentDialogue = dialogueInteractionBehaviour.LoseDialogue;
                            GameStateBehaviour.Instance.playerSprite.sprite = playerKetchup;

                        }
                        else
                        {
                            DialogueBox.Instance.currentDialogue = dialogueInteractionBehaviour.WinDialogue;
                            GameStateBehaviour.Instance.raVitoSprite.sprite = raVitoKetchup;
                        }
                        GameStateBehaviour.Instance.player.toInteract = GameStateBehaviour.Instance.raVitoInteraction.GetComponent<DialogueInteractionBehaviour>();
                        GameStateBehaviour.Instance.ChangeRavitoDialogue(4);
                        DialogueBox.Instance.setOriginalText();
                    });
                });

            });
        }
    }
}
