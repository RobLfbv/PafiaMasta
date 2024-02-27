using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private YetteRunning yette;
    [SerializeField]
    private ChronoBehaviour chrono;
    [SerializeField]
    public GameObject particleConfettis;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.RunMiniGame)
        {
            chrono.stopTimer = true;
            yette.playerWin = true;
            particleConfettis.SetActive(true);
        }
    }
}
