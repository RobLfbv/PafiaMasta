using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        Dialogue
    }

    public GameState currentState;
    public GameObject dialogueScreen;
    public GameObject pauseScreen;
    public bool isPaused = false;

    void Start()
    {
        ChangeToMainGame();
        UnpauseGame();
    }

    public void ChangeToDialogue()
    {
        currentState = GameState.Dialogue;
        dialogueScreen.SetActive(true);
    }

    public void ChangeToMainGame()
    {
        currentState = GameState.MainGame;
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
    }
}
