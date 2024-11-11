using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Student
{
    public class OOPPlayer : MonoBehaviour
    {
        public float speed = 5;
        private Animator animator;
        public int health = 100;  // Player's health
        public int maxHealth = 100;  // Max health of the player

        private void Start()
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component missing on player.");
            }

            if (GetComponent<Rigidbody2D>() == null)
            {
                Debug.LogError("Rigidbody2D component missing on player.");
            }
        }

        private void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 dir = new Vector2(moveX, moveY).normalized;

            // Set animation direction
            if (moveX < 0) animator.SetInteger("Direction", 3);  // Left
            else if (moveX > 0) animator.SetInteger("Direction", 2); // Right
            else if (moveY > 0) animator.SetInteger("Direction", 1); // Up
            else if (moveY < 0) animator.SetInteger("Direction", 0); // Down

            animator.SetBool("IsMoving", dir.magnitude > 0);

            // Apply velocity based on the direction
            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

        // Method to heal the player
        public void Heal(int healAmount, bool isBonus = false)
        {
            if (isBonus)
            {
                healAmount *= 2;  // Double the healing amount for bonus potions
            }

            health += healAmount;

            // Ensure health doesn't exceed maxHealth
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            // Log the healing to console
            Debug.Log("Player healed by " + healAmount + ". Current health: " + health);
        }

        // Method for taking damage
        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            Debug.Log("Player damaged by " + damageAmount + ". Current health: " + health);
        }

        // Handle player death
        private void Die()
        {
            // Trigger death animation or other effects
            animator.SetTrigger("Die");  // Assuming you have a "Die" trigger in your Animator

            // Optionally: Disable player controls, show game over screen, etc.
            this.enabled = false;  // Disable player control after death
        }
    }
}
