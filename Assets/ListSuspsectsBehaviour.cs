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
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }
        if (index - 1 < 0)
        {
            backButton.interactable = false;
            //nextButton.navigation.selectOnLeft = quitButton;
        }
        else
        {
            backButton.interactable = true;
        }
    }
}
