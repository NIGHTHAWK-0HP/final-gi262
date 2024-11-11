using System.Collections;
using UnityEngine;

namespace Student
{
    public class EnemyAI : MonoBehaviour
    {
        public Transform player; // Reference to the player
        public float moveSpeed = 2f; // Enemy movement speed
        public float attackRange = 1.5f; // Range attacks player
        public float attackCooldown = 1f; // Cooldown attacks
        public int attackDamage = 10; // Damage dealt to the player per attack
        private bool canAttack = true; // enemy attack
        private Character CharacterHealth; // Reference to the player's health
        private Rigidbody2D rb;

        private void Start()
        {
            // If player is not manually assigned in the Inspector, find it by tag
            if (player == null)
            {
                GameObject playerObj = GameObject.FindWithTag("Player");
                if (playerObj != null)
                {
                    player = playerObj.transform;
                }
                else
                {
                    Debug.LogError("Player GameObject with 'Player' tag not found.");
                }
            }

            // Now safely try to get the player's health component
            if (player != null)
            {
                CharacterHealth = player.GetComponent<Character>();
                if (CharacterHealth == null)
                {
                    Debug.LogError("CharacterHealth component is missing on the player object.");
                }
            }
            else
            {
                Debug.LogError("Player Transform is not assigned.");
            }

            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from Enemy object.");
            }
        }


        private void Update()
        {
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                FollowPlayer(distanceToPlayer);
                CheckAttackPlayer(distanceToPlayer);
            }
        }

        private void FollowPlayer(float distanceToPlayer)
        {
            // If the player is out of attack range, move towards the player
            if (distanceToPlayer > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;

                // Use Rigidbody2D for smoother movement
                if (rb != null)
                {
                    rb.velocity = direction * moveSpeed;
                }
                else
                {
                    transform.position =
                        Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                }

                // Face the player (optional)
                FacePlayer(direction);
            }
            else
            {
                // Stop movement if within attack range
                if (rb != null) rb.velocity = Vector2.zero;
            }
        }

        private void CheckAttackPlayer(float distanceToPlayer)
        {
            // If the player is within attack range and attack cooldown has passed
            if (distanceToPlayer <= attackRange && canAttack)
            {
                StartCoroutine(AttackPlayer());
            }
        }

        private IEnumerator AttackPlayer()
        {
            canAttack = false;

            // If player health exists, deal damage
            if (CharacterHealth != null)
            {
                CharacterHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy attacks the player, dealing " + attackDamage + " damage!");
                // Optional: Trigger attack animation here
            }

            // Wait for cooldown before allowing another attack
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        // Method to make the enemy face the player
        private void FacePlayer(Vector2 direction)
        {
            if (direction.x < 0)
            {
                // If the player is to the left, flip the enemy sprite (if needed)
                transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
            }
            else
            {
                // If the player is to the right, reset to normal orientation
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
