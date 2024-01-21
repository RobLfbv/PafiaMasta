using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class TesTransi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().DOFade(0, 3).OnComplete(() => gameObject.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
