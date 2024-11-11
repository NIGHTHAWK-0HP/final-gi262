using UnityEngine;

public class EnemyHealth2D : MonoBehaviour
{
    public int maxHP = 50;    // HP สูงสุดของศัตรู
    public int currentHP;     // HP ปัจจุบัน

    void Start()
    {
        currentHP = maxHP; // ตั้งค่า HP เริ่มต้น
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ลด HP ตามความเสียหายที่ได้รับ
        Debug.Log("Enemy HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die(); // ถ้า HP = 0 ศัตรูตาย
        }
    }

    void Die()
    {
        Debug.Log("Enemy has died!");
        Destroy(gameObject); // ทำลายศัตรูเมื่อ HP เป็นศูนย์
    }
}