using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoice : MonoBehaviour
{

    private AudioSource source;

    public void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }
}
