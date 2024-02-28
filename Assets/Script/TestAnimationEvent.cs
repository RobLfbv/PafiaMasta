using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class TestAnimationEvent : MonoBehaviour
{
    public void ChangeSprite()
    {
        GetComponent<Animator>().enabled = false;
        DialogueBox.Instance.Talker2Sprite();
    }
}
