using UnityEngine;

public class OOPPlayer : Character
{
    public int maxHealth = 100;  // Player's max health
    private int currentHealth;

    public float moveSpeed = 5f;  // Movement speed of the player
    private Rigidbody2D rb;  // Reference to the player's Rigidbody2D component
    private Vector2 moveDirection;  // Direction the player is moving in

    private void Start()
    {
        currentHealth = maxHealth;  // Initialize health
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing on player.");
        }
    }

    private void Update()
    {
        HandleMovement();  // Handle player movement
    }

    private void HandleMovement()
    {
        // Get input for horizontal and vertical movement (WSAD or Arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");  // -1 for left, 1 for right
        float moveY = Input.GetAxisRaw("Vertical");  // -1 for down, 1 for up

        // Store the movement direction in a Vector2
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Apply movement to the player's Rigidbody2D (without using Physics)
        rb.velocity = moveDirection * moveSpeed;
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;

        // Ensure health doesn't exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Player healed by " + healingAmount + ". Current health: " + currentHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);  // Call the base method to reduce health

        if (currentHealth <= 0)
        {
            Die();  // Call the overridden Die method when health reaches 0
        }
    }

    // Override the Die method
    public override void Die()
    {
        Debug.Log("Player has died.");
        // You can add custom logic here (e.g., respawn, game over)
        Destroy(gameObject);  // Default action: destroy the player object
    }
}
