using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class TestAnimationEvent : MonoBehaviour
{
    public void ChangeSprite()
    {
        print("aaa");
        DialogueBox.Instance.Talker2Sprite();
        //transform.DORotate(30,3,RotateMode)
    }
}
