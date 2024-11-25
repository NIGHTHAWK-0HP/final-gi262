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
    
    public LayerMask obstacleLayer; // กำหนด Layer สำหรับ Tile ที่เป็นสิ่งกีดขวาง
    public float obstacleAvoidanceRadius = 0.5f; // รัศมีตรวจจับสิ่งกีดขวาง
    
    private Vector2 lastPosition;
    private float stuckTimer = 0f;
    private float stuckThreshold = 2f; // เวลาที่ถือว่าติด

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
    
        // ตรวจสอบสิ่งกีดขวางรอบตัว
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position + (Vector3)direction, obstacleAvoidanceRadius, obstacleLayer);
    
        if (obstacle != null)
        {
            Debug.Log("Obstacle detected! Adjusting path.");
    
            // ปรับทิศทางหลบโดยหมุน 90 องศา
            Vector2 avoidDirection = Vector2.Perpendicular(direction);
            rb.velocity = avoidDirection * moveSpeed;
        }
        else
        {
            rb.velocity = direction * moveSpeed;
        }
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
    
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction);
    
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + (Vector3)direction, obstacleAvoidanceRadius);
        }
    }
    
    private void CheckStuck()
    {
        if (Vector2.Distance(transform.position, lastPosition) < 0.1f)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckThreshold)
            {
                Debug.Log("Enemy stuck! Adjusting...");
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.velocity = randomDirection * moveSpeed;
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }
        lastPosition = transform.position;
    }


    public void SetDamage(int newDamage)
    {
        attackDamage = newDamage;
        Debug.Log("Enemy damage set to: " + attackDamage);
    }
}
