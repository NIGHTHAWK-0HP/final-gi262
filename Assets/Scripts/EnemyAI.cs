using UnityEngine;

public class EnemyAI : Character
{
    public float moveSpeed = 3f;  // ความเร็วในการเคลื่อนที่ของศัตรู
    public GameObject player;     // เปลี่ยนเป็น GameObject แทน Transform
    public float rotationSpeed = 5f;  // ความเร็วในการหมุนของศัตรู
    public float attackRange = 1.5f;  // ระยะในการโจมตี
    public int attackDamage = 20;    // ความเสียหายในการโจมตี

    private Rigidbody2D rb;    // Rigidbody2D สำหรับเคลื่อนที่

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing on the enemy.");
        }
    }

    private void Update()
    {
        // ตรวจสอบระยะห่างจากผู้เล่น
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

                // ตรวจสอบว่า GameObject player มี tag "Player"
                if (player.CompareTag("Player"))
                {
                    AttackPlayer();
                }
            }
        }
    }

    private void FollowPlayer()
    {
        // คำนวณทิศทางไปยังผู้เล่น
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // คำนวณมุมระหว่างศัตรูและผู้เล่น
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // หมุนศัตรูไปในทิศทางของผู้เล่น
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // เคลื่อนที่ไปยังทิศทางของผู้เล่น
        rb.velocity = direction * moveSpeed;
    }

    private void StopMoving()
    {
        // หยุดการเคลื่อนที่เมื่ออยู่ในระยะโจมตี
        rb.velocity = Vector2.zero;
    }

    private void AttackPlayer()
    {
        // ตรวจสอบว่า GameObject player มี tag "Player"
        if (player.CompareTag("Player"))
        {
            // เรียกใช้ TakeDamage จาก Character หรือ Player class
            Character characterScript = player.GetComponent<Character>();
            if (characterScript != null)
            {
                characterScript.TakeDamage(attackDamage);
                Debug.Log("Enemy attacked the player for " + attackDamage + " damage.");
            }
            else
            {
                Debug.LogError("Character script not found on the player object.");
            }
        }
    }
}

