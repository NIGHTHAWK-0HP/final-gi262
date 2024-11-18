using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health = 100; // Base health for all characters

    // Mark Die as virtual so it can be overridden
    public virtual void Die()
    {
        Debug.Log("Character died.");
        Destroy(gameObject); // Default behavior: destroy the object
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();  // Call the Die method when health reaches 0
        }
    }
}
