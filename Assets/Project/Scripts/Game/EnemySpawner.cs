using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Requirements")]
    public GameObject enemyContainer;
    public GameController controller;
    public RoundController roundController;

    [Header("Gameplay")]
    public float spawnTime = 5f;
    public int enemyModifier = 5;
    public bool roundStart = true;


    private int totalEnemies;
    public int TotalEnemies { get{ return totalEnemies; } }

    public int spawnedEnemies = 0;

    void LateUpdate()
    {
        if (roundStart && !roundController.betweenRounds)
        {
            totalEnemies = enemyModifier * roundController.CurrentRound;
            StartCoroutine(SpawnEnemies());
            roundStart = false;
        }

        int aliveEnemies = 0;
        foreach (ZombieEnemy_DamageReceiver enemy in enemyContainer.GetComponentsInChildren<ZombieEnemy_DamageReceiver>())
        {
            if (enemy.Dead == false)
            {
                aliveEnemies++;
            }
        }

        if (aliveEnemies == 0 && spawnedEnemies == totalEnemies && !roundController.betweenRounds)
        {
            roundController.NextRound();
        }
    }

    public IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemies)
        {
            foreach (SpawnPoint spawnPoint in controller.GetComponentsInChildren<SpawnPoint>())
            {

                spawnPoint.EnemySpawn();
                yield return new WaitForSeconds(spawnTime);

            }
        }
    }

    public void NewRound(int round)
    {
        roundController.betweenRounds = false;
        roundStart = true;
        spawnedEnemies = 0;
    }
}
