using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private GameObject optionMenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject creditsMenu;
    [SerializeField]
    private GameObject resetMenu;
    [SerializeField]
    private Button backButtonCredits;
    [SerializeField]
    private Button backButtonReset;
    [SerializeField]
    private Button backButtonOption;
    [SerializeField]
    private Button resetButtonOption;
    [SerializeField]
    private InputSystemUIInputModule uiInput;

    private void FixedUpdate()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Vector2 vec = uiInput.move.ToInputAction().ReadValue<Vector2>();
            if (vec != Vector2.zero)
            {
                if (optionMenu.activeSelf)
                {
                    backButtonOption.Select();
                }
                else if (mainMenu.activeSelf)
                {
                    buttons[0].Select();
                }
                else if (creditsMenu.activeSelf)
                {
                    backButtonCredits.Select();
                }
                else if (resetMenu.activeSelf)
                {
                    backButtonReset.Select();
                }
            }
        }
    }

    private void Start()
    {
        buttons[0].Select();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void RetourOption()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        buttons[1].Select();
    }
    public void RetourCredits()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        buttons[2].Select();
    }
    public void RetourReset()
    {
        resetMenu.SetActive(false);
        optionMenu.SetActive(true);
        resetButtonOption.Select();
    }
    public void Credits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        backButtonCredits.Select();
    }
    public void Option()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
        backButtonOption.Select();
    }
    public void Reset()
    {
        resetMenu.SetActive(true);
        optionMenu.SetActive(false);
        backButtonReset.Select();
    }
    public void DoReset()
    {
        PlayerPrefs.DeleteAll();
        RetourReset();
    }


}
