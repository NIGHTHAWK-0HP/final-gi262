using UnityEngine;

public class WaveLifetime : MonoBehaviour
{
    public float lifetime = 0.5f;  // เวลาที่คลื่นจะอยู่ในโลก (ในวินาที)
    public int damage = 10;        // ความเสียหายที่คลื่นจะทำกับศัตรู

    void Start()
    {
        // ลบคลื่นหลังจาก lifetime
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าคลื่นชนกับศัตรูหรือไม่
        if (other.CompareTag("Enemy"))  // ตรวจสอบว่าเป็นศัตรู (ตรวจสอบจาก tag "Enemy")
        {
            Character enemy = other.GetComponent<Character>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // ทำความเสียหายให้กับศัตรู
                Debug.Log("Wave hits the enemy for " + damage + " damage.");
                Destroy(gameObject);
            }
        }
    }
}