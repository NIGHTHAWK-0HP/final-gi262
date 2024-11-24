using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOPPlayer : Character
{
    public float moveSpeed = 5f; // Movement speed of the player
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // The point where projectiles are spawned
    public float projectileSpeed = 10f; // Speed of the projectile
    public float fireRate = 0.5f; // Delay between shots

    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component
    private Vector2 moveDirection; // Direction the player is moving in
    private float currentSpeed; // The player's current speed
    private Coroutine speedCoroutine; // To manage the speed boost duration
    private float lastFireTime; // Timestamp of the last shot fired

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        HandleMovement();  // Handle player movement
        HandleAiming();    // Rotate Fire Point to face mouse
        HandleShooting();  // Handle player shooting
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * currentSpeed;
    }

    private void HandleAiming()
    {
        if (firePoint != null)
        {
            // Get mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
            // Calculate the direction from the Fire Point to the mouse
            Vector2 direction = (mousePosition - firePoint.position).normalized;
    
            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    
            // Rotate only the Fire Point to face the mouse
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time > lastFireTime + fireRate)
        {
            ShootProjectile();
            lastFireTime = Time.time;
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.velocity = firePoint.right * projectileSpeed;
            }
        }
    }

    public void IncreaseSpeed(float amount, float duration)
    {
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }
        speedCoroutine = StartCoroutine(SpeedBoostCoroutine(amount, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float amount, float duration)
    {
        currentSpeed += amount;
        yield return new WaitForSeconds(duration);
        currentSpeed -= amount;
    }
}
