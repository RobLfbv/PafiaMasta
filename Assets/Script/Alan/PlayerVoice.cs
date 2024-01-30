using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoice : MonoBehaviour
{
    [SerializeField]
    AudioClip[] listeSons;
    private AudioSource source;

    public void OnEnable()
    {
        source = GetComponent<AudioSource>();
        PlayRandom();
    }

    void PlayRandom()
    {
        int idSon = Random.Range(0, listeSons.Length);
        source.clip = listeSons[idSon];
        source.Play();
    }
}
