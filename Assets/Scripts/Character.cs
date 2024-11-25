using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public int maxHealth = 100; // พลังชีวิตสูงสุด
    public int health = 100;    // พลังชีวิตปัจจุบัน
    private int currentHealth;

    // เรียกเมื่อพลังชีวิตหมด
    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        OnCharacterDied();
    }

    // สามารถ override เพื่อเปลี่ยนพฤติกรรมหลังจากตายได้
    protected virtual void OnCharacterDied()
    {
        // ตรวจสอบว่าอ็อบเจ็กต์ที่ถูกทำลายคือลักษณะของเพลย์เยอร์
        if (gameObject.CompareTag("Player"))
        {
            // เปลี่ยนฉากไปยัง "GameOver" เมื่อเพลย์เยอร์ตาย
            SceneManager.LoadScene("GameOver");  // เปลี่ยนชื่อฉากเป็นที่คุณต้องการ
        }
        else
        {
            // สำหรับตัวละครอื่นๆ สามารถเพิ่มการทำงานได้ที่นี่
            Destroy(gameObject);  // ทำลายอ็อบเจ็กต์สำหรับตัวละครที่ไม่ใช่เพลย์เยอร์
        }
    }

    // รับความเสียหาย
    public virtual void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return; // ตัวละครตายแล้ว

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // ฟื้นฟูพลังชีวิต
    public void Heal(int amount)
    {
        if (currentHealth <= 0) return; // ไม่สามารถฟื้นฟูตัวละครที่ตายแล้วได้

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // จำกัดพลังชีวิตไม่ให้เกินค่าสูงสุด
        }

        Debug.Log($"{gameObject.name} healed {amount}. Health: {currentHealth}");
    }
    protected virtual void Start()
    {
        currentHealth = maxHealth;  // เริ่มต้นค่าพลังชีวิต
    }
}
