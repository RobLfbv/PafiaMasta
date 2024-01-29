using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChronoBehaviour1 : MonoBehaviour
{
    public float timeTotal;
    public bool stopTimer = false;
    public TMP_Text timerText;

    private void OnEnable()
    {
        stopTimer = false;
    }

    private void OnDisable()
    {
        stopTimer = true;
        timeTotal = 0;
    }

    void Update()
    {
        if (!stopTimer)
        {
            timeTotal -= Time.deltaTime;
            DisplayTime(timeTotal);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = timeToDisplay % 1 * 1000;
        timerText.text = string.Format("{0:00}:{1:000}", seconds, milliSeconds);
    }
}
