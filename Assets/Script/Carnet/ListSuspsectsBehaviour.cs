using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSuspsectsBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> suspects;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button quitButton;
    private int index = 0;
    [SerializeField]
    private List<Button> alibiButtons;
    [SerializeField]
    private List<Button> mobileButtons;
    [SerializeField]
    private int currentMenuSelection;
    
    [SerializeField]
    private List<GameObject> alibiMenu;
    
    [SerializeField]
    private List<GameObject> mobileMenu;
    private void OnEnable()
    {
        DesactivateSuspect();
        index = 0;
        VerificationButton();
        suspects[index].SetActive(true);
        nextButton.Select();
    }

    private void DesactivateSuspect()
    {
        for (int i = 0; i < suspects.Count; i++)
        {
            suspects[i].SetActive(false);
        }
    }

    public void NextPage()
    {
        DesactivateSuspect();
        index++;
        suspects[index].gameObject.SetActive(true);
        VerificationButton();
    }
    public void PreviousPage()
    {
        DesactivateSuspect();
        index--;
        suspects[index].gameObject.SetActive(true);
        VerificationButton();
    }
    
    public void ActivateAlibi()
    {
        alibiMenu[index].SetActive(false);
        mobileMenu[index].SetActive(true);
        mobileButtons[index].Select();
    }
    public void ActivateMobile()
    {
        alibiMenu[index].SetActive(true);
        mobileMenu[index].SetActive(false);
        alibiButtons[index].Select();
    }

    private void VerificationButton()
    {
        if (index + 1 == suspects.Count)
        {
            /*Navigation navigationBack = backButton.navigation;
            Navigation navigationResume = resumeButton.navigation;
            navigationBack.selectOnRight = mobileButtons[index];
            navigationResume.selectOnLeft = backButton;
            backButton.navigation = navigationBack;
            resumeButton.navigation = navigationResume;*/
            //nextButton.gameObject.SetActive(false);
            nextButton.enabled = false;
            backButton.Select();
        }
        else
        {
            /*Navigation navigationBack = backButton.navigation;
            Navigation navigationResume = resumeButton.navigation;
            navigationBack.selectOnRight = mobileButtons[index];
            navigationResume.selectOnLeft = nextButton;
            backButton.navigation = navigationBack;
            resumeButton.navigation = navigationResume;*/
            //nextButton.gameObject.SetActive(true);
            nextButton.enabled = true;

        }
        if (index - 1 < 0)
        {
            /*Navigation navigationNext = nextButton.navigation;
            Navigation navigationQuit = quitButton.navigation;
            //navigationNext.selectOnLeft = quitButton;
            navigationNext.selectOnLeft = mobileButtons[index];
            navigationQuit.selectOnRight = nextButton;
            nextButton.navigation = navigationNext;
            quitButton.navigation = navigationQuit;*/
            //backButton.gameObject.SetActive(false);
            backButton.enabled = false;
            nextButton.Select();
        }
        else
        {
            /*Navigation navigationNext = nextButton.navigation;
            Navigation navigationQuit = quitButton.navigation;
            //navigationNext.selectOnLeft = backButton;
            navigationNext.selectOnLeft = mobileButtons[index];

            navigationQuit.selectOnRight = backButton;
            nextButton.navigation = navigationNext;
            quitButton.navigation = navigationQuit;*/
            backButton.enabled = true;
            //backButton.gameObject.SetActive(true);
        }
    }
}
