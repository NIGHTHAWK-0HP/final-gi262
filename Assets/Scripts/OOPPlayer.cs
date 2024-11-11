using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Student
{
    public class OOPPlayer : MonoBehaviour

    {
        public float speed;
        private Animator animator;
        public int health = 100;  // Player's health
        public int maxHealth = 100;  // Max health of the player

        // Method to heal the player
        public void Heal(int healAmount, bool isBonus = false)
        {
            if (isBonus)
            {
                healAmount *= 2;  // Double the healing amount for bonus potions
            }

            health += healAmount;

            // Ensure health doesn't exceed maxHealth
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            // Log the healing to console
            Debug.Log("Player healed by " + healAmount + ". Current health: " + health);
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }
}