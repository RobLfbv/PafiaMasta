using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDialogueAction : MonoBehaviour, ISelectHandler
{
    public int goToID;

    public void ButtonAction()
    {
        DialogueBox.Instance.GoToNextDialogue(goToID);
        DialogueBox.Instance.SetCurrentDialogue();
    }

    public void OnSelect(BaseEventData eventData)
    {
        print("ah");
    }
}
