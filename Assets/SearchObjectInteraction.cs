using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObjectInteraction : MonoBehaviour
{
    [SerializeField]
    public GameObject keyInteraction;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<CharacterBehaviour>().takenObject != null && GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.SearchMiniGame)
        {
            other.GetComponent<CharacterBehaviour>().canInteract = true;
            other.GetComponent<CharacterBehaviour>().searchObject = this;
            keyInteraction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<CharacterBehaviour>().takenObject != null && GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.SearchMiniGame)
        {
            col.GetComponent<CharacterBehaviour>().canInteract = false;
            col.GetComponent<CharacterBehaviour>().searchObject = null;
            keyInteraction.SetActive(false);

        }
    }
}
