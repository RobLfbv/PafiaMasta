using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDialogueAction : MonoBehaviour
{
    public int goToID;

    public void ButtonAction()
    {
        DialogueBox.Instance.GoToNextDialogue(goToID);
        DialogueBox.Instance.SetCurrentDialogue();
    }
}
