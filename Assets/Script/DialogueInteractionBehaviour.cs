using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueInteractionBehaviour : MonoBehaviour
{
    [SerializeField]
    public DialogueScriptableObject dialogueData;
    [SerializeField]
    public GameObject keyInteraction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterBehaviour>().canInteract = true;
            other.GetComponent<CharacterBehaviour>().toInteract = this;
            keyInteraction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<CharacterBehaviour>().canInteract = false;
            col.GetComponent<CharacterBehaviour>().toInteract = null;
            keyInteraction.SetActive(false);

        }
    }
}
