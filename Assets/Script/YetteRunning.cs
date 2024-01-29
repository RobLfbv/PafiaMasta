using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class YetteRunning : MonoBehaviour
{

    [SerializeField]
    private Transform[] pos;
    [SerializeField]
    private Transform ogPosYette;
    [SerializeField]
    private Transform resetPosPlayer;
    [SerializeField]
    private Transform player;
    private int currentIdx;
    private Vector2 vectorDir;
    [SerializeField]
    private float speed;
    private bool stop;
    [SerializeField]
    private Image transition;
    [HideInInspector]
    public bool playerWin;
    public DialogueInteractionBehaviour dialogueInteractionBehaviour;

    [SerializeField]
    private Animator anim;
    private string clipname;

    void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("running", true);
        
        currentIdx = 0;
        vectorDir = pos[currentIdx].position - transform.position;
        vectorDir = new Vector2(vectorDir.x, vectorDir.y);
        vectorDir = new Vector2(vectorDir.x / vectorDir.magnitude, vectorDir.y / vectorDir.magnitude);
        playerWin = false;
        stop = false;
    }

    private void FixedUpdate()
    {
        clipname = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (!stop && Vector2.Distance(transform.position, pos[currentIdx].position) > 1f && clipname == "YetteRunning")
        {
            transform.Translate(vectorDir * speed);
        }
        else if (!stop && clipname == "YetteRunning")
        {
            currentIdx++;
            if (currentIdx < pos.Length)
            {
                vectorDir = pos[currentIdx].position - transform.position;
                vectorDir = new Vector2(vectorDir.x, vectorDir.y);
                vectorDir = new Vector2(vectorDir.x / vectorDir.magnitude, vectorDir.y / vectorDir.magnitude);
            }
            else
            {
                anim.SetBool("running", false);

                stop = true;
                transition.gameObject.SetActive(true);
                transition.DOFade(1, 1).OnComplete(() =>
                {
                    transform.position = ogPosYette.position;
                    player.position = resetPosPlayer.position;
                    GameStateBehaviour.Instance.ChangeToDialogue();
                    if (playerWin)
                    {
                        DialogueBox.Instance.currentDialogue = dialogueInteractionBehaviour.WinDialogue;
                        GameStateBehaviour.Instance.ChangeYeetDialogue(4);
                    }
                    else
                    {
                        DialogueBox.Instance.currentDialogue = dialogueInteractionBehaviour.LoseDialogue;
                    }
                    DialogueBox.Instance.setOriginalText();
                    player.GetComponent<CharacterBehaviour>().rb.velocity = Vector2.zero;
                    transition.DOFade(0f, 1f).OnComplete(() =>
                    {

                        transition.gameObject.SetActive(false);
                    });

                });
            }
        }
    }
}
