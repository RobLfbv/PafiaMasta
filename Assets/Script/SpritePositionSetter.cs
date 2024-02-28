using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }
    void SetPosition()
    {
        // If you want to change the transform, use this
        Vector3 newPosition = transform.position;
        newPosition.z = transform.position.y;
        transform.position = newPosition;

        /* Or if you want to change the SpriteRenderer's sorting order, use this
        GetComponent<SpriteRenderer>().sortingOrder = (int)transform.position.y;*/
    }
}
