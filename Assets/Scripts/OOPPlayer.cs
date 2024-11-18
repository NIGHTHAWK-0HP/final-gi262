using System.Collections;
using UnityEngine;

namespace Student
{
    public class OOPPlayer : Character
    {
        public float speed = 5;
        public int maxHealth = 100;  // Max health of the player
        private int currentHealth;    // Current health of the player

        private Animator animator;
        private Rigidbody2D rb;

        private void Start()
        {
            // Cache components for efficiency
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;  // Initialize current health

            // Error checking for missing components
            if (animator == null)
            {
                Debug.LogError("Animator component missing on player.");
            }
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing on player.");
            }
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            // Movement input
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            // Normalize the direction vector to maintain consistent speed
            Vector2 direction = new Vector2(moveX, moveY).normalized;

            // Update animation based on movement direction
            UpdateAnimator(moveX, moveY, direction);

            // Apply movement velocity
            rb.velocity = speed * direction;
        }

        private void UpdateAnimator(float moveX, float moveY, Vector2 direction)
        {
            // Set animation based on movement direction
            if (moveX < 0) animator.SetInteger("Direction", 3);  // Left
            else if (moveX > 0) animator.SetInteger("Direction", 2); // Right
            else if (moveY > 0) animator.SetInteger("Direction", 1); // Up
            else if (moveY < 0) animator.SetInteger("Direction", 0); // Down

            animator.SetBool("IsMoving", direction.magnitude > 0);
        }
        public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    // Method to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Additional logic for player death (e.g., restarting the game, playing animations, etc.)
    }
}

        // Method to heal the player
        public void Heal(int healAmount, bool isBonus = false)
        {
            if (isBonus)
            {
                healAmount *= 2;  // Double the healing amount for bonus potions
            }

            // Increase health and clamp it to maxHealth
            currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

            Debug.Log("Player healed by " + healAmount + ". Current health: " + currentHealth);
        }
    }
}

