using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    private CircleCollider2D colliderInteraction;

    private void Start()
    {
        colliderInteraction = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<CharacterBehaviour>().canInteract = true;
            col.GetComponent<CharacterBehaviour>().toInteract = this;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<CharacterBehaviour>().canInteract = false;
            col.GetComponent<CharacterBehaviour>().toInteract = this;
        }
    }
}
