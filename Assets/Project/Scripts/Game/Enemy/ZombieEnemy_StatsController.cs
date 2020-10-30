using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy_StatsController : MonoBehaviour
{
    //This class needs to be moved to sit on the round controller object in order to keep the health and damage numbers persistant
    public float startingHealth = 100;
    public float startingDamage = 15;
    public float currentDamage;
    public float currentHealth;
    public int pointValue = 100;
    
    void Awake()
    {
        currentDamage = startingDamage;
        currentHealth = startingHealth;
    }

    public void NewRound(int round)
    {
        currentDamage = currentDamage + (float)(round * .5);
        currentHealth = currentHealth + (float)(round * .5);
    }
}
