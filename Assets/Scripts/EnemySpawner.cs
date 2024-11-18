using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 20f;
    public int enemiesPerWave = 5;
    public float spawnDelay = 0.1f;
    public float spawnRadius = 2f;  // ระยะที่ใช้ในการตรวจสอบการซ้อนทับ

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
                    Vector3 spawnPosition = GetValidSpawnPosition();
                    SpawnEnemy(spawnPosition);
                    yield return new WaitForSeconds(spawnDelay);
                }

                Debug.Log("Wave Finished!");
                isSpawning = false;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition = spawnPoint.position;
        int attempts = 0;

        // ตรวจสอบตำแหน่งหลายครั้งเพื่อป้องกันการซ้อนทับ
        while (Physics2D.OverlapCircle(spawnPosition, spawnRadius) != null && attempts < 5)
        {
            // เปลี่ยนตำแหน่งแบบสุ่มภายในระยะที่กำหนด
            spawnPosition = spawnPoint.position + (Vector3)Random.insideUnitCircle * spawnRadius;
            attempts++;
        }

        return spawnPosition;
    }

    void SpawnEnemy(Vector3 spawnPosition)
    {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}