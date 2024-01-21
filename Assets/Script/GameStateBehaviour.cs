using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameStateBehaviour : MonoBehaviour
{
    //*****
    // Singleton pattern
    //*****
    private static GameStateBehaviour _instance;
    public static GameStateBehaviour Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    //*****
    // Singleton pattern
    //*****

    public enum GameState
    {
        MainGame,
        Dialogue,
        RunMiniGame,
    }
    public GameState currentState;
    [Header("Dialogue Variables")]
    public GameObject dialogueScreen;
    [Header("Pause Variables")]
    public GameObject pauseScreen;
    public bool isPaused = false;

    [Header("Run Mini Game Variables")]
    public GameObject runMiniGameScreen;
    public GameObject yette;
    public GameObject yetteInteraction;

    void Start()
    {
        ChangeToMainGame();
        UnpauseGame();
        PlayerPrefs.DeleteAll();
        /*PlayerPrefs.SetInt("Yette", 1);
        PlayerPrefs.SetInt("Zily",1);
        PlayerPrefs.SetInt("Farfolle",1);
        PlayerPrefs.SetInt("Ghetti",1);
        PlayerPrefs.SetInt("Ravito",1);*/
    }

    public void ChangeToDialogue()
    {
        currentState = GameState.Dialogue;
        dialogueScreen.SetActive(true);
        runMiniGameScreen.SetActive(false);
        yette.GetComponent<YetteRunning>().enabled = false;
    }

    public void ChangeToMainGame()
    {
        currentState = GameState.MainGame;
        runMiniGameScreen.SetActive(false);
        yette.GetComponent<YetteRunning>().enabled = false;
        dialogueScreen.SetActive(false);
    }

    public void ChangeToRunMiniGame()
    {
        currentState = GameState.RunMiniGame;
        runMiniGameScreen.SetActive(true);
        yette.GetComponent<YetteRunning>().enabled = true;
        yetteInteraction.SetActive(false);
        dialogueScreen.SetActive(false);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    /*
    0 = LaunchGameDialogue
    1 = NotUnlock
    2 = WinDialogue
    3 = LoseDialogue
    4 = FinishGameDialogue
    5 = AllMiniGameDoneDialogue
    6 = YetteDialogue
    */
    public void ChangeYeetDialogue(int idxDialogue)
    {
        PlayerPrefs.SetInt("Yette", idxDialogue);
    }
}
