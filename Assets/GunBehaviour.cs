using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class GunBehaviour : MonoBehaviour
{
    public bool playerTurn;
    public GameObject gun;

    private void Start()
    {
        playerTurn = false;
    }

    public void GunAnimation()
    {
        gun.transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 360f), 1f, RotateMode.WorldAxisAdd);
        int result = Random.Range(0, 6);
        if (result != 0)
        {
            print("Nothing Happen");
            playerTurn = !playerTurn;
        }
        else
        {
            if (playerTurn)
            {
                print("The player is dead");
            }
            else
            {
                print("Ra vito is dead");
            }
        }
    }
}
