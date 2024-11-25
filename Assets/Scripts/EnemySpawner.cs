using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 20f;
    public int minEnemiesPerWave = 3;   // จำนวนศัตรูขั้นต่ำในแต่ละ wave
    public int maxEnemiesPerWave = 6;   // จำนวนศัตรูสูงสุดในแต่ละ wave
    public float spawnDelay = 0.1f;
    public float spawnRadius = 2f;

    public int baseDamage = 10;         // ค่าความเสียหายเริ่มต้น
    public int damageIncrement = 5;    // จำนวนที่เพิ่มขึ้นในแต่ wave
    public int baseHealth = 100;         // ค่าพลังชีวิตเริ่มต้น
    public int healthIncrement = 20;    // จำนวนที่เพิ่มขึ้นในแต่ wave
    public int maxWaves = 3;           // จำนวนสูงสุดของ wave

    private bool isSpawning = false;
    private int currentWave = 0;     

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (currentWave < maxWaves)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                currentWave++;
                Debug.Log($"Wave {currentWave} Started! Damage: {baseDamage}, Health: {baseHealth}");

                // Randomize number of enemies per wave between min and max
                int enemiesInThisWave = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);

                for (int i = 0; i < enemiesInThisWave; i++)  // ใช้ค่าที่สุ่มได้จาก Random.Range
                {
                    Vector3 spawnPosition = GetValidSpawnPosition();
                    SpawnEnemy(spawnPosition);
                    yield return new WaitForSeconds(spawnDelay);
                }

                Debug.Log($"Wave {currentWave} Finished!");
                isSpawning = false;

                // เพิ่มความเสียหายและพลังชีวิตสำหรับ next wave
                baseDamage += damageIncrement;
                baseHealth += healthIncrement;

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        Debug.Log("All waves finished!");
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition = spawnPoint.position;
        int attempts = 0;

        while (Physics2D.OverlapCircle(spawnPosition, spawnRadius) != null && attempts < 5)
        {
            spawnPosition = spawnPoint.position + (Vector3)Random.insideUnitCircle * spawnRadius;
            attempts++;
        }

        return spawnPosition;
    }

    void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // ส่งค่าความเสียหายและพลังชีวิตให้กับ EnemyAI
        EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
        if (enemyScript != null)
        {
            enemyScript.SetDamage(baseDamage);
            enemyScript.SetHealth(baseHealth);
        }
    }
}
