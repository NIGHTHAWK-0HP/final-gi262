using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(gameObject); // ทำลายวัตถุ
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

    // ใช้ public หรือ protected แทน private เพื่อให้คลาสลูกสามารถเข้าถึงได้
    protected virtual void Start()
    {
        currentHealth = maxHealth;  // เริ่มต้นค่าพลังชีวิต
    }
}
