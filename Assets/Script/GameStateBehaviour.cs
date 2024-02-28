using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameStateBehaviour : MonoBehaviour
{
    public AudioClip[] musics;
    public AudioSource cam;

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
        GunMiniGame,
        FactoryMiniGame
    }
    public CharacterBehaviour player;
    public Image transition;
    public SmoothFollow smoothCamera;
    public GameState currentState;
    public DialogueScriptableObject introDialogue;
    public GameObject mailiMailoInteraction;
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
    public SpriteRenderer playerSprite;
    public SpriteRenderer raVitoSprite;

    [Header("Factory Mini Game Variables")]
    public GameObject factoryMiniGameScreen;
    public GameObject zilyInteraction;

    [Header("Riddle Mini Game Variables")]
    public GameObject ghettiInteraction;
    public InputDevice currentController;

    [Header("UIKeyboard")]
    [SerializeField]
    private GameObject[] UIKeyboard;

    [Header("UIGamepad")]
    [SerializeField]
    private GameObject[] UIGamepad;


    /*
    0 = LaunchGameDialogue
    1 = NotUnlock
    2 = WinDialogue
    3 = LoseDialogue
    4 = FinishGameDialogue
    5 = InfoDialogue
    */
    void Start()
    {
        ChangeToMainGame();
        UnpauseGame();
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("AlreadyLaunched") == 0)
        {
            PlayerPrefs.SetInt("AlreadyLaunched", 1);
            PlayerPrefs.SetInt("Yette", 1);
            PlayerPrefs.SetInt("Zily", 1);
            PlayerPrefs.SetInt("Farfolle", 1);
            PlayerPrefs.SetInt("Ghetti", 1);
            PlayerPrefs.SetInt("Ra_Vito", 0);
            PlayerPrefs.SetInt("Maili_Mailo", 1);
            ChangeToDialogue();
            DialogueBox.Instance.currentDialogue = introDialogue;
            DialogueBox.Instance.setOriginalText();
        }

    }

    public void ChangeToDialogue()
    {
        if (cam.clip != musics[0])
        {
            cam.clip = musics[0];
            cam.Play();
        }

        currentState = GameState.Dialogue;
        dialogueScreen.SetActive(true);
        runMiniGameScreen.SetActive(false);
        pistolMiniGameScreen.SetActive(false);
        factoryMiniGameScreen.SetActive(false);
        raVitoInteraction.SetActive(true);
        zilyInteraction.SetActive(true);
        raVitoGun.SetActive(false);
        mailiMailoInteraction.SetActive(true);
        yette.GetComponent<YetteRunning>().enabled = false;
        searchMiniGameScreen.SetActive(false);
    }

    public void ChangeToMainGame()
    {
        if (cam.clip != musics[0])
        {
            cam.clip = musics[0];
            cam.Play();
        }

        currentState = GameState.MainGame;
        runMiniGameScreen.SetActive(false);
        factoryMiniGameScreen.SetActive(false);
        pistolMiniGameScreen.SetActive(false);
        yette.GetComponent<YetteRunning>().enabled = false;
        dialogueScreen.SetActive(false);
        yetteInteraction.SetActive(true);
        zilyInteraction.SetActive(true);
        ghettiInteraction.SetActive(true);
        raVitoInteraction.SetActive(true);
        farfolleInteraction.SetActive(true);
        mailiMailoInteraction.SetActive(true);
        for (int i = 0; i < searchObjectInteractions.Length; i++)
            searchObjectInteractions[i].enabled = false;
    }

    public void ChangeToRunMiniGame()
    {
        cam.clip = musics[3];
        cam.Play();

        currentState = GameState.RunMiniGame;
        runMiniGameScreen.SetActive(true);
        yette.GetComponent<YetteRunning>().enabled = true;
        yetteInteraction.SetActive(false);
        yetteUIInteraction.SetActive(false);
        zilyInteraction.SetActive(false);
        ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        dialogueScreen.SetActive(false);
        mailiMailoInteraction.SetActive(false);
    }

    public void ChangeToSearchMiniGame()
    {
        currentState = GameState.SearchMiniGame;
        searchMiniGameScreen.SetActive(true);
        yetteInteraction.SetActive(false);
        zilyInteraction.SetActive(false);
        ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        mailiMailoInteraction.SetActive(false);
        for (int i = 0; i < searchObjectInteractions.Length; i++)
            searchObjectInteractions[i].enabled = true;
        dialogueScreen.SetActive(false);
    }

    public void ChangeToGunMiniGame()
    {
        cam.clip = musics[1];
        cam.Play();

        player.canShoot = false;
        currentState = GameState.GunMiniGame;
        yetteInteraction.SetActive(false);
        zilyInteraction.SetActive(false);
        ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        transition.gameObject.SetActive(true);
        transition.DOFade(1, 1).OnComplete(() =>
        {
            player.transform.position = gunBehaviour.posGame.position;
            pistolMiniGameScreen.SetActive(true);
            raVitoGun.SetActive(true);
            smoothCamera.follow = raVitoGun;
            transition.DOFade(0, 1).OnComplete(() =>
            {
                transition.gameObject.SetActive(true);
                gunBehaviour.key.SetActive(true);
                player.canShoot = true;
            });
        });
        for (int i = 0; i < searchObjectInteractions.Length; i++)
            searchObjectInteractions[i].enabled = true;
        dialogueScreen.SetActive(false);
    }

    public void ChangeToFactoryMiniGame()
    {
        cam.clip = musics[2];
        cam.Play();

        currentState = GameState.FactoryMiniGame;
        factoryMiniGameScreen.SetActive(true);
        yetteInteraction.SetActive(false);
        zilyInteraction.SetActive(false);
        ghettiInteraction.SetActive(false);
        raVitoInteraction.SetActive(false);
        farfolleInteraction.SetActive(false);
        dialogueScreen.SetActive(false);
        player.nextInput = Vector2.right;
        StartCoroutine(player.DoAfterDelay(1f, player.CalculateRPM));
        player.ChangeObjective();
    }

    public void ButtonPauseGame()
    {
        if (Time.timeScale != 0)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }

    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void WinRiddleGame()
    {
        ChangeZilyDialogue(5);
        ChangeGhettiDialogue(4);
        print("WinRiddleGame :" + (PlayerPrefs.GetInt("Ra_Vito") == 0));
        print("WinRiddleGame :" + PlayerPrefs.GetInt("Ra_Vito"));
        if (PlayerPrefs.GetInt("Ra_Vito") == 0)
        {
            PlayerPrefs.SetInt("RaVitoNextInfo", 1);
        }
        else
        {
            ChangeRavitoDialogue(5);
        }
    }
    public void EndingRaVito()
    {
        SceneManager.LoadScene(2);
    }

    public void EndingYette()
    {
        SceneManager.LoadScene(3);
    }

    public void DialogueYetteOmbre()
    {
        PlayerPrefs.SetInt("YetteInfoDialogueDone", PlayerPrefs.GetInt("YetteInfoDialogueDone") + 1);
        PlayerPrefs.SetInt("YetteInfoDialogueDoneOmbre", 1);
        PlayerPrefs.SetInt("FarfolleUnlock3", 1);
        if (PlayerPrefs.GetInt("YetteInfoDialogueDone") == 2)
        {
            ChangeYeetDialogue(0);
        }
    }
    public void DialogueYetteRaVito()
    {
        PlayerPrefs.SetInt("YetteInfoDialogueDone", PlayerPrefs.GetInt("YetteInfoDialogueDone") + 1);
        PlayerPrefs.SetInt("YetteInfoDialogueDoneRaVito", 1);
        PlayerPrefs.SetInt("FarfolleUnlock2", 1);
        if (PlayerPrefs.GetInt("YetteInfoDialogueDone") == 2)
        {
            ChangeYeetDialogue(0);
        }
    }

    public void CheckYetteDialogueInfoZily()
    {
        PlayerPrefs.SetInt("YetteInfoDialogueUnlock", PlayerPrefs.GetInt("YetteInfoDialogueUnlock") + 1);
        if (PlayerPrefs.GetInt("YetteInfoDialogueUnlock") == 3)
        {
            ChangeYeetDialogue(5);
        }
    }
    public void CheckYetteDialogueInfoRaVito()
    {
        PlayerPrefs.SetInt("YetteInfoDialogueUnlock", PlayerPrefs.GetInt("YetteInfoDialogueUnlock") + 1);
        if (PlayerPrefs.GetInt("YetteInfoDialogueUnlock") == 3)
        {
            ChangeYeetDialogue(5);
        }
    }
    public void CheckYetteDialogueInfoFarfolle()
    {
        PlayerPrefs.SetInt("YetteInfoDialogueUnlock", PlayerPrefs.GetInt("YetteInfoDialogueUnlock") + 1);
        if (PlayerPrefs.GetInt("YetteInfoDialogueUnlock") == 3)
        {
            ChangeYeetDialogue(5);
        }
    }
    /*
    0 = LaunchGameDialogue
    1 = NotUnlock
    2 = WinDialogue
    3 = LoseDialogue
    4 = FinishGameDialogue
    5 = InfoDialogue
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
        if (idxDialogue == 4 && PlayerPrefs.GetInt("RaVitoNextInfo") == 1)
        {
            PlayerPrefs.SetInt("Ra_Vito", 5);
        }
        else
        {
            PlayerPrefs.SetInt("Ra_Vito", idxDialogue);
        }
    }
    public void ChangeZilyDialogue(int idxDialogue)
    {
        PlayerPrefs.SetInt("Zily", idxDialogue);
    }
    public void ChangeMailiMailoDialogue(int idxDialogue)
    {
        PlayerPrefs.SetInt("Maili_Mailo", idxDialogue);
    }
    public void ChangeGhettiDialogue(int idxDialogue)
    {
        PlayerPrefs.SetInt("Ghetti", idxDialogue);
    }

    public void ChangeCharDialogue(Character charToChange, int idxDialogue)
    {
        PlayerPrefs.SetInt(charToChange.ToString(), idxDialogue);
    }

    public void ChangeController(InputDevice newController)
    {
        currentController = newController;
        if (currentController.name.Contains("Controller"))
        {
            foreach (GameObject gb in UIGamepad)
                gb.SetActive(true);

            foreach (GameObject gb in UIKeyboard)
                gb.SetActive(false);
        }
        else if (currentController.name.Contains("Keyboard"))
        {
            foreach (GameObject gb in UIGamepad)
                gb.SetActive(false);

            foreach (GameObject gb in UIKeyboard)
                gb.SetActive(true);
        }
    }

}
