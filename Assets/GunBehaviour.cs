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

    private void Start()
    {
        playerTurn = false;
    }

    public void GunAnimation()
    {
        gun.transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.WorldAxisAdd).OnComplete(() =>
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
        });
        int result = Random.Range(0, 6);
        if (result != 0)
        {
            playerTurn = !playerTurn;
            gun.GetComponent<SpriteRenderer>().flipY = !playerTurn;
            if (mainCamera.orthographicSize > 2.5f)
            {
                mainCamera.orthographicSize -= 0.5f;
            }
        }
        else
        {
            transitionWhite.gameObject.SetActive(true);
            transitionWhite.DOFade(1, 1f).OnComplete(() =>
            {
                player.transform.position = posAfterGame.position;
                mainCamera.orthographicSize = 5f;
                gun.SetActive(false);
                transitionWhite.DOFade(1, 1f).OnComplete(() =>
                {

                    transitionWhite.DOFade(0, 3).OnComplete(() =>
                    {
                        transitionWhite.gameObject.SetActive(false);
                        GameStateBehaviour.Instance.ChangeToDialogue();
                        if (playerTurn)
                        {
                            DialogueBox.Instance.currentDialogue = dialogueInteractionBehaviour.LoseDialogue;
                        }
                        else
                        {
                            DialogueBox.Instance.currentDialogue = dialogueInteractionBehaviour.WinDialogue;
                        }
                        GameStateBehaviour.Instance.ChangeRavitoDialogue(4);
                        DialogueBox.Instance.setOriginalText();
                    });
                });

            });


        }
    }
}
