using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class TesTransi : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().DOFade(0, 3).OnComplete(() => gameObject.SetActive(false));
    }
}
