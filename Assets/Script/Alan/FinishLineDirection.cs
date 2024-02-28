using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.U2D.IK;

public class FinishLineDirection : MonoBehaviour
{
    public float limX;
    public float limY;
    public float speed;
    public GameObject finishLine;

    void Update()
    {
        //follow FinishLine
        Vector2 newPos = finishLine.transform.position - transform.position;
        transform.Translate(newPos * Time.deltaTime * speed);

        //limit
        //gauche
        if(transform.localPosition.x > limX)
        {
            transform.localPosition = new Vector2(limX, transform.localPosition.y);
        }
        //haut
        if(transform.localPosition.y > limY)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, limY);
        }
        //droite
        if (transform.localPosition.x < -limX)
        {
            transform.localPosition = new Vector2(-limX, transform.localPosition.y);
        }
        //bas
        if (transform.localPosition.y < -limY)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, -limY);
        }
    }
}
