using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG;
using DG.Tweening;


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
        SearchMiniGame,
        GunMiniGame
    }
    public CharacterBehaviour player;
    public Image transition;
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
    public GameObject yetteUIInteraction;

    [Header("Search Mini Game Variables")]
    public GameObject searchMiniGameScreen;
    public GameObject farfolleInteraction;
    public SearchObjectInteraction[] searchObjectInteractions;

    [Header("Pistol Mini Game Variables")]
    public GameObject pistolMiniGameScreen;
    public GameObject raVitoInteraction;
    public GameObject raVitoGun;
    public GunBehaviour gunBehaviour;

    [Header("Factory Mini Game Variables")]
    public GameObject factoryMiniGameScreen;
    public GameObject zilyInteraction;

    [Header("Riddle Mini Game Variables")]
    public GameObject riddleMiniGameScreen;
    public GameObject ghettiInteraction;


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
        pistolMiniGameScreen.SetActive(false);
        raVitoInteraction.SetActive(true);
        raVitoGun.SetActive(false);
        yette.GetComponent<YetteRunning>().enabled = false;
        searchMiniGameScreen.SetActive(false);
    }

    public void ChangeToMainGame()
    {
        currentState = GameState.MainGame;
        runMiniGameScreen.SetActive(false);
        pistolMiniGameScreen.SetActive(false);
        yette.GetComponent<YetteRunning>().enabled = false;
        dialogueScreen.SetActive(false);
        yetteInteraction.SetActive(true);
        //zilyInteraction.SetActive(true);
        //ghettiInteraction.SetActive(true);
        raVitoInteraction.SetActive(true);
        farfolleInteraction.SetActive(true);
        for (int i = 0; i < searchObjectInteractions.Length; i++)
            searchObjectInteractions[i].enabled = false;
    }

    public void ChangeToRunMiniGame()
    {
        currentState = GameState.RunMiniGame;
        runMiniGameScreen.SetActive(true);
        yette.GetComponent<YetteRunning>().enabled = true;
        yetteInteraction.SetActive(false);
        yetteUIInteraction.SetActive(false);
        //zilyInteraction.SetActive(false);
        //ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        dialogueScreen.SetActive(false);

    }

    public void ChangeToSearchMiniGame()
    {
        currentState = GameState.SearchMiniGame;
        searchMiniGameScreen.SetActive(true);
        yetteInteraction.SetActive(false);
        //zilyInteraction.SetActive(false);
        //ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        for (int i = 0; i < searchObjectInteractions.Length; i++)
            searchObjectInteractions[i].enabled = true;
        dialogueScreen.SetActive(false);
    }

    public void ChangeToGunMiniGame()
    {
        currentState = GameState.GunMiniGame;
        pistolMiniGameScreen.SetActive(true);
        yetteInteraction.SetActive(false);
        //zilyInteraction.SetActive(false);
        //ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        transition.gameObject.SetActive(true);
        transition.DOFade(1, 1).OnComplete(() =>
        {
            player.transform.position = gunBehaviour.posGame.position;
            raVitoGun.SetActive(true);
            transition.DOFade(0, 1).OnComplete(() => transition.gameObject.SetActive(true));
        });
        for (int i = 0; i < searchObjectInteractions.Length; i++)
            searchObjectInteractions[i].enabled = true;
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
    public void ChangeFarfolleDialogue(int idxDialogue)
    {
        PlayerPrefs.SetInt("Farfolle", idxDialogue);
    }
    public void ChangeRavitoDialogue(int idxDialogue)
    {
        PlayerPrefs.SetInt("Ravito", idxDialogue);
    }
}
