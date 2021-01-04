using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public EnemySpawner enemySpawner;
    public GameObject enemyContainer;
    public GameObject enemy;
    
    void Start()
    {
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        enemyContainer = GameObject.Find("EnemyContainer");
    }

    public void EnemySpawn()
    {
            if (enemySpawner.spawnedEnemies < enemySpawner.TotalEnemies)
            {
                GameObject newEnemy = Instantiate(enemy, this.transform.position, this.transform.rotation);
                newEnemy.name = "Enemy";
                newEnemy.transform.parent = enemyContainer.transform;
                enemySpawner.spawnedEnemies++;
            }
    }
}
