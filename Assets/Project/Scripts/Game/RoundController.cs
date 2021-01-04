using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public RoundMenu roundMenu;
    public GameController gameController;
    public bool betweenRounds = false;

    private int currentRound = 1;
    public int CurrentRound { get { return currentRound; } }
    
    public void NextRound()
    {
        StartCoroutine(NewRoundDelay());
    }

    public IEnumerator NewRoundDelay()
    {
        betweenRounds = true;
        yield return new WaitForSeconds(5f);
        currentRound++;
        gameController.BroadcastMessage("NewRound", currentRound);
    }
}
