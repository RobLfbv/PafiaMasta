using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.U2D.IK;
using static UnityEngine.GraphicsBuffer;

public class FinishLineDirection : MonoBehaviour
{
    public float hideDistance;
    public Transform finishLine;

    void Update()
    {
        //c le code d'un tuto
        Vector2 newPos = finishLine.position - transform.position;
        if (newPos.magnitude < hideDistance)
        {
            SetChildrenActive(false);
        }
        else
        {
            SetChildrenActive(true);
            var angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
