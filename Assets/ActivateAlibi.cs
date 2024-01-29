using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAlibi : MonoBehaviour
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private GameObject alibi1;
    [SerializeField]
    private GameObject alibi2;
    [SerializeField]
    private GameObject alibi3;
    [SerializeField]
    private GameObject alibi4;
    [SerializeField]
    private GameObject alibi5;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(character.ToString() + "Alibi1") == 1)
        {
            alibi1.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(character.ToString() + "Alibi2") == 1)
        {
            alibi2.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(character.ToString() + "Alibi3") == 1)
        {
            alibi3.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(character.ToString() + "Alibi4") == 1)
        {
            alibi4.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(character.ToString() + "Alibi5") == 1)
        {
            alibi5.SetActive(true);
        }
    }
}
