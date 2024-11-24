using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOPPlayer : Character
{
    public float moveSpeed = 5f;  // Movement speed of the player
    private Rigidbody2D rb;  // Reference to the player's Rigidbody2D component
    private Vector2 moveDirection;  // Direction the player is moving in

    protected override void Start()
        {
            base.Start(); // เรียกใช้ฟังก์ชัน Start() จากคลาส Character
            // การตั้งค่าที่เฉพาะสำหรับ OOPPlayer
            rb = GetComponent<Rigidbody2D>();
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
}
