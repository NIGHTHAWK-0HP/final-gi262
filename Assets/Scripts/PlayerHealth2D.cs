using UnityEngine;

public class PlayerHealth2D : MonoBehaviour
{
    public int maxHP = 100;    // HP สูงสุด
    public int currentHP;      // HP ปัจจุบัน

    void Start()
    {
        currentHP = maxHP; // ตั้งค่า HP เริ่มต้น
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ลด HP ตามความเสียหายที่ได้รับ
        Debug.Log("Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die(); // ถ้า HP = 0 ตัวละครจะตาย
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        // ทำให้ตัวละครตาย (เช่น หยุดการเคลื่อนไหว หรือเล่นแอนิเมชันการตาย)
        gameObject.SetActive(false); // ตัวละครตายและหายไป
    }
}