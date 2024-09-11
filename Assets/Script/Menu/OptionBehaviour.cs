using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class OptionBehaviour : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private Button frButton;
    [SerializeField]
    private Button enButton;

    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("ChoosedLang")];
        dropdown.value = PlayerPrefs.GetInt("ChoosedLang");

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

    public void ChangeLang()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
        PlayerPrefs.SetInt("ChoosedLang", dropdown.value);
    }
    public void ChangeToFr()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        PlayerPrefs.SetInt("ChoosedLang", 1);
        dropdown.value = PlayerPrefs.GetInt("ChoosedLang");
        enButton.interactable = true;
        frButton.interactable = false;
    }

    public void ChangeToEn()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        PlayerPrefs.SetInt("ChoosedLang", 0);
        dropdown.value = PlayerPrefs.GetInt("ChoosedLang");
        frButton.interactable = true;
        enButton.interactable = false;
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
