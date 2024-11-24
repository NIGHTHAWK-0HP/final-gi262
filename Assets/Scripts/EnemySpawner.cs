using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 20f;
    public int minEnemiesPerWave = 3;   // จำนวนศัตรูขั้นต่ำในแต่ละระลอก
    public int maxEnemiesPerWave = 6;   // จำนวนศัตรูสูงสุดในแต่ละระลอก
    public float spawnDelay = 0.1f;
    public float spawnRadius = 2f;

    public int baseDamage = 10;         // ค่าความเสียหายเริ่มต้น
    public int damageIncrement = 10;    // จำนวนที่เพิ่มขึ้นในแต่ละระลอก
    public int maxWaves = 10;           // จำนวนสูงสุดของระลอก

    private bool isSpawning = false;
    private int currentWave = 0;        // ระบุระลอกปัจจุบัน

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (currentWave < maxWaves)  // ตรวจสอบให้แน่ใจว่าไม่เกินจำนวนระลอกที่กำหนด
        {
            if (!isSpawning)
            {
                isSpawning = true;
                currentWave++;
                Debug.Log($"Wave {currentWave} Started! Damage: {baseDamage}");

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

                // เพิ่มความเสียหายสำหรับระลอกถัดไป
                baseDamage += damageIncrement;

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        Debug.Log("All waves finished!"); // เมื่อเสร็จสิ้นทุกระลอก
        Debug.Log("You Win!");
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

        // ส่งค่าความเสียหายให้กับ EnemyAI
        EnemyAI enemyScript = enemy.GetComponent<EnemyAI>();
        if (enemyScript != null)
        {
            enemyScript.SetDamage(baseDamage);
        }
    }
}
