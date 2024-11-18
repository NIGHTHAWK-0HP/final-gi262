using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public GameObject swordPrefab;  // สร้าง sword prefab
    public Transform player;        // อ้างอิงตำแหน่งของ Player

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
    
    void Start() 
    {
        player = GameObject.FindWithTag("Player").transform;
    }
}