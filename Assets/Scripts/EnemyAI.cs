using UnityEngine;

public class EnemyAI : Character
{
    public float moveSpeed = 3f; 
    public GameObject player;    
    public float rotationSpeed = 5f; 
    public float attackRange = 1.5f; 
    public int attackDamage = 20;   
    public float attackCooldown = 1f;  // Time between attacks

    private Rigidbody2D rb;
    private Character playerCharacterScript;
    private float lastAttackTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing on the enemy.");
        }
        
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player not found.");
            }
            else
            {
                playerCharacterScript = player.GetComponent<Character>();  // Cache the player's Character script
                if (playerCharacterScript == null)
                {
                    Debug.LogError("Character script not found on player.");
                }
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > attackRange)
            {
                FollowPlayer();
            }
            else
            {
                StopMoving();

                if (player.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.velocity = direction * moveSpeed;
    }

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void AttackPlayer()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if (player.CompareTag("Player") && playerCharacterScript != null)
            {
                playerCharacterScript.TakeDamage(attackDamage);
                lastAttackTime = Time.time; // Reset attack cooldown timer
                Debug.Log("Enemy attacks the player for " + attackDamage + " damage.");
            }
        }
    }
}

