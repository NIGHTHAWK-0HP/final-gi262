using UnityEngine;

public class EnemyAI : Character
{
    public float moveSpeed = 3f; 
    public GameObject player;    
    public float attackRange = 1.5f; 
    public int attackDamage = 10;   
    public float attackCooldown = 1f; 

    private Rigidbody2D rb;
    private Character playerCharacterScript;
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
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
                playerCharacterScript = player.GetComponent<Character>();
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

                if (Time.time > lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // No rotation logic, so we just move towards the player without rotating
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
            if (playerCharacterScript != null)
            {
                playerCharacterScript.TakeDamage(attackDamage);
                lastAttackTime = Time.time;
                Debug.Log("Enemy attacks the player for " + attackDamage + " damage.");
            }
        }
    }

    public void SetDamage(int newDamage)
    {
        attackDamage = newDamage;
        Debug.Log("Enemy damage set to: " + attackDamage);
    }
}
