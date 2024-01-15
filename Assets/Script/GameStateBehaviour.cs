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
    void Start()
    {
        currentState = GameState.MainGame;
    }

    public void ChangeToDialogue()
    {
        currentState = GameState.Dialogue;
        dialogueScreen.SetActive(true);
    }
}
