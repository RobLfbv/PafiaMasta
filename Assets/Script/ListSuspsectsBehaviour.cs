using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSuspsectsBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Image> suspects;
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
    private int currentMenuSelection;
    private void OnEnable()
    {
        DesactivateSuspect();
        index = 0;
        VerificationButton();
        suspects[index].gameObject.SetActive(true);
        nextButton.Select();
    }

    private void DesactivateSuspect()
    {
        for (int i = 0; i < suspects.Count; i++)
        {
            suspects[i].gameObject.SetActive(false);
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
    private void VerificationButton()
    {
        if (index + 1 == suspects.Count)
        {
            Navigation navigationBack = backButton.navigation;
            Navigation navigationResume = resumeButton.navigation;
            navigationBack.selectOnRight = resumeButton;
            navigationResume.selectOnLeft = backButton;
            backButton.navigation = navigationBack;
            resumeButton.navigation = navigationResume;
            nextButton.gameObject.SetActive(false);
            backButton.Select();
        }
        else
        {
            Navigation navigationBack = backButton.navigation;
            Navigation navigationResume = resumeButton.navigation;
            navigationBack.selectOnRight = nextButton;
            navigationResume.selectOnLeft = nextButton;
            backButton.navigation = navigationBack;
            resumeButton.navigation = navigationResume;
            nextButton.gameObject.SetActive(true);
        }
        if (index - 1 < 0)
        {
            Navigation navigationNext = nextButton.navigation;
            Navigation navigationQuit = quitButton.navigation;
            navigationNext.selectOnLeft = quitButton;
            navigationQuit.selectOnRight = nextButton;
            nextButton.navigation = navigationNext;
            quitButton.navigation = navigationQuit;
            backButton.gameObject.SetActive(false);
            nextButton.Select();
        }
        else
        {
            Navigation navigationNext = nextButton.navigation;
            Navigation navigationQuit = quitButton.navigation;
            navigationNext.selectOnLeft = backButton;
            navigationQuit.selectOnRight = backButton;
            nextButton.navigation = navigationNext;
            quitButton.navigation = navigationQuit;
            backButton.gameObject.SetActive(true);
        }
    }
}
