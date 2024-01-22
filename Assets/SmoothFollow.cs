using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public GameObject follow;
    private Vector3 velocity = Vector3.zero;
    public float maxSpeed = 0.2f;
    public BoxCollider2D boundsCollider;
    private Vector3 targetPos;
    void Update()
    {
        targetPos = follow.transform.position;
        if (!boundsCollider.bounds.Contains(targetPos))
        {
            targetPos = boundsCollider.bounds.ClosestPoint(targetPos);
        }
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPos + new Vector3(0, 0, -10f), ref velocity, maxSpeed);
    }
}
