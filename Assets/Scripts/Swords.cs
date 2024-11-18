using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public GameObject swordPrefab;  // สร้าง sword prefab
    public GameObject shotWavePrefab;  // สร้าง shot wave prefab
    public Transform player;        // อ้างอิงตำแหน่งของ Player
    public float shotWaveSpeed = 10f; // ความเร็วของ shot wave

    // ฟังก์ชั่นนี้จะทำงานเมื่อมีการชนกับ Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าเป็น Player ที่ชนกับ Object นี้
        if (other.CompareTag("Player"))
        {
            // สร้าง sword ที่ตำแหน่ง attach ของ Player
            GameObject sword = Instantiate(swordPrefab, player.position, Quaternion.identity);

            // ตั้งค่าให้ sword เป็นลูกของผู้เล่น
            sword.transform.SetParent(player);

            // ปรับตำแหน่งของ sword relative กับผู้เล่น (ตำแหน่งในมือหรือข้างๆ)
            sword.transform.localPosition = new Vector3(0.5f, 0.5f, 0f); // ปรับค่าตำแหน่งตามต้องการ
            sword.transform.localRotation = Quaternion.Euler(0, 0, -30f);  // ปรับการหมุนให้ดาบเอียงไปตามที่ต้องการ

            // ทำลาย Object นี้หลังจากที่ทำงานเสร็จ
            Destroy(gameObject);
        }
    }

    // ฟังก์ชั่นการยิง shot wave
    void ShootShotWave(Vector2 direction)
    {
        // สร้าง shot wave ที่ตำแหน่งของผู้เล่น
        GameObject shotWave = Instantiate(shotWavePrefab, player.position, Quaternion.identity);

        // เพิ่มความเร็วให้กับ shot wave โดยการคำนวณทิศทางการยิง
        Rigidbody2D rb = shotWave.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * shotWaveSpeed;
        }
    }

    void Start() 
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // ตรวจสอบการกดปุ่ม Left Click
        if (Input.GetMouseButtonDown(0)) // 0 หมายถึงการคลิกซ้าย
        {
            // คำนวณตำแหน่งของเมาส์ในโลก
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // คำนวณทิศทางจากผู้เล่นไปยังเมาส์
            Vector2 direction = (mousePosition - (Vector2)player.position).normalized;

            // ยิง shot wave ตามทิศทางที่คำนวณได้
            ShootShotWave(direction);
        }
    }
}