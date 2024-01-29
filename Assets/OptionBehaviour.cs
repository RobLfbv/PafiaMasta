using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionBehaviour : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }
    public void Load()
    {
        slider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
