using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public RoundMenu roundMenu;
    public GameController gameController;

    private int currentRound;
    public int CurrentRound { get { return currentRound; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRound()
    {
        roundMenu.Pause();
        currentRound++;
        gameController.BroadcastMessage("NewRound", currentRound);
    }
}
