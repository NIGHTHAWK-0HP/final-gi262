using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public Transform spawnPoint;    // The point where enemies will spawn
    public float timeBetweenWaves = 5f;  // Time between each wave
    public int enemiesPerWave = 5;  // Number of enemies per wave
    public float spawnDelay = 1f;  // Delay between spawning each enemy in the wave

    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                Debug.Log("Wave Started!");

                for (int i = 0; i < enemiesPerWave; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnDelay);
                }

                Debug.Log("Wave Finished!");
                isSpawning = false;
                yield return new WaitForSeconds(timeBetweenWaves);  // Wait before next wave
            }
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
