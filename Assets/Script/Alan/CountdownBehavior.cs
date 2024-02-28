using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownBehavior : MonoBehaviour
{
    public float startTimer;
    public TMP_Text timerText;
    public bool stopTimer = false;
    public AudioClip[] sounds;

    private float timeTotal;
    private AudioSource source;
    private string oldDigit;

    private void OnEnable()
    {
        stopTimer = false;
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
        //écrire
        if (seconds < 1)
        {
            timerText.text = "GO";
        }
        else
        {
            timerText.text = "" + seconds;
        }

        //son
        if(oldDigit != timerText.text)
        {
            if(timerText.text == "GO")
            {
                source.clip = sounds[1];
            }
            else source.clip = sounds[0];
            source.Play();
        }


        //bye bye
        if(seconds < 0)
        {
            gameObject.SetActive(false);
        }

    }
}




    

