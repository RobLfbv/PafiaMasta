using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MiniGameToLaunch
{
    RunMiniGame,
    FactoryMiniGame,
    GunMiniGame
}
public class CountdownBehavior : MonoBehaviour
{
    public float startTimer;
    public TMP_Text timerText;
    public bool stopTimer = false;
    public AudioClip[] sounds;

    private float timeTotal;
    private AudioSource source;
    private string oldDigit;
    public MiniGameToLaunch miniGameToLaunch;

    private void OnEnable()
    {
        stopTimer = false;
        timeTotal = startTimer;
        source = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        stopTimer = true;
        timeTotal = startTimer;
        oldDigit = null;
    }

    void Update()
    {
        if (!stopTimer)
        {
            oldDigit = timerText.text;
            timeTotal -= Time.deltaTime;
            DisplayTime(timeTotal);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //�crire
        if (seconds < 1)
        {
            timerText.text = "GO";
        }
        else
        {
            timerText.text = "" + seconds;
        }

        //son
        if (oldDigit != timerText.text)
        {
            if (timerText.text == "GO")
            {
                source.clip = sounds[1];
            }
            else source.clip = sounds[0];
            source.Play();
        }


        //bye bye
        if (seconds < 0)
        {   switch(miniGameToLaunch){
            case MiniGameToLaunch.RunMiniGame:
                GameStateBehaviour.Instance.runMiniGameScreen.SetActive(true);
                GameStateBehaviour.Instance.yette.GetComponent<YetteRunning>().enabled = true;
                GameStateBehaviour.Instance.canMove = true;
                break;
            
            case MiniGameToLaunch.GunMiniGame:
                GameStateBehaviour.Instance.transition.gameObject.SetActive(true);
                GameStateBehaviour.Instance.gunBehaviour.key.SetActive(true);
                GameStateBehaviour.Instance.player.canShoot = true;
                break;
            
            case MiniGameToLaunch.FactoryMiniGame:
                GameStateBehaviour.Instance.currentState = GameStateBehaviour.GameState.FactoryMiniGame;
                GameStateBehaviour.Instance.factoryMiniGameScreen.SetActive(true);
                GameStateBehaviour.Instance.transition.gameObject.SetActive(false);
                GameStateBehaviour.Instance.player.nextInput = Vector2.right;
                StartCoroutine(GameStateBehaviour.Instance.player.DoAfterDelay(1f, GameStateBehaviour.Instance.player.CalculateRPM));
                GameStateBehaviour.Instance.player.ChangeObjective();
                break;
        }
            gameObject.SetActive(false);
        }

    }
}






