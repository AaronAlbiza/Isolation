using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public EnemySpawner enemySpawner;
    public GameObject enemyContainer;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        enemyContainer = GameObject.Find("EnemyContainer");
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(enemySpawner.roundStart);
        if (enemySpawner.roundStart)
        {
           StartCoroutine(EnemySpawn());
        }*/
    }

    public void EnemySpawn()
    {
            if (enemySpawner.spawnedEnemies < enemySpawner.TotalEnemies)
            {
                GameObject newEnemy = Instantiate(enemy, this.transform.position, this.transform.rotation);
                newEnemy.name = "Enemy";
                newEnemy.transform.parent = enemyContainer.transform;
                //yield return new WaitForSeconds(enemySpawner.spawnTime);
                enemySpawner.spawnedEnemies++;
                //Debug.Log("Spawned enemies " + enemySpawner.spawnedEnemies + " at position " + this.transform.position);
            }
    }
}
