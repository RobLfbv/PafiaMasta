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

    private float zpos;
    private float xrota;
    private float yrota;

    void Start()
    {
        zpos = transform.localPosition.z;

        xrota = transform.localRotation.x;
        yrota = transform.localRotation.y;
    }
    void Update()
    {
        Debug.Log(transform.localPosition + " - " + transform.localRotation);
        //follow FinishLine
        Vector2 newPos = finishLine.transform.position - transform.position;
        transform.Translate(newPos * Time.deltaTime * speed);

        //rotate
        transform.LookAt(finishLine.transform.position);

        //limit pos XY
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

        //limit Z
        if (transform.localPosition.z != zpos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zpos);
        }

        //limit rotation
        if (transform.localRotation.x != xrota || transform.localRotation.y != yrota)
        {
            transform.localRotation = new Quaternion(xrota, yrota, transform.localRotation.z, transform.localRotation.w);
        }

    }
}
