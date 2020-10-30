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

    private int round = 1;
    public int Round { get{ return round; } }

    public int spawnedEnemies = 0;

    void LateUpdate()
    {
        if (roundStart)
        {
            totalEnemies = enemyModifier * round;
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

        if (aliveEnemies == 0 && spawnedEnemies == totalEnemies)
        {
            roundController.NextRound();
        }
    }

    public IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < TotalEnemies)
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
        roundStart = true;
        spawnedEnemies = 0;
    }
}
