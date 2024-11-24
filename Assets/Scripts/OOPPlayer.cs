using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOPPlayer : Character
{
    public float moveSpeed = 5f;  // Movement speed of the player
    private Rigidbody2D rb;  // Reference to the player's Rigidbody2D component
    private Vector2 moveDirection;  // Direction the player is moving in

    private float currentSpeed;  // The player's current speed (to handle temporary speed boosts)
    private Coroutine speedCoroutine;  // To manage the speed boost duration

    protected override void Start()
    {
        base.Start(); // Call the Start() function from the base Character class
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component for movement
        currentSpeed = moveSpeed;  // Set the current speed to the default move speed
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
        rb.velocity = moveDirection * currentSpeed;  // Use current speed for movement
    }

    // Method to temporarily increase the player's speed
    public void IncreaseSpeed(float amount, float duration)
    {
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);  // If there's already an active speed boost, stop it
        }

        speedCoroutine = StartCoroutine(SpeedBoostCoroutine(amount, duration));  // Start a new speed boost
    }

    // Coroutine to handle the speed boost duration
    private IEnumerator SpeedBoostCoroutine(float amount, float duration)
    {
        currentSpeed += amount;  // Increase the player's speed

        yield return new WaitForSeconds(duration);  // Wait for the duration of the boost

        currentSpeed -= amount;  // Reset the player's speed back to normal after the boost ends
    }
}

