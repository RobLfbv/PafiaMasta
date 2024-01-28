using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Vector2 ogPos;

    private void Start()
    {
        ogPos = transform.position;
    }
}
