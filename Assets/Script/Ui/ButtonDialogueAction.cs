using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDialogueAction : MonoBehaviour, ISelectHandler
{
    public int goToID;

    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void ButtonAction()
    {
        DialogueBox.Instance.GoToNextDialogue(goToID);
        DialogueBox.Instance.SetCurrentDialogue();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (source != null)
            source.Play();
    }

    public void ChangeSelectedChar()
    {
        GameStateBehaviour.Instance.charSelected = name;
    }
}
