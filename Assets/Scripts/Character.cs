using UnityEngine;

public class Character : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth; // Ensure health doesn't exceed maxHealth
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}