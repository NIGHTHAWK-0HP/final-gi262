using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Student
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;
        public float speed;
        private Animator animator;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            PrintInfo();
            GetRemainEnergy();
        }

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 dir = new Vector2(horizontal, vertical).normalized;
    
            if (dir.magnitude > 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            // Setting Direction based on input
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                animator.SetInteger("Direction", horizontal > 0 ? 2 : 3); // Right = 2, Left = 3
            }
            else
            {
                animator.SetInteger("Direction", vertical > 0 ? 1 : 0); // Up = 1, Down = 0
            }

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }


        public void Attack(OOPEnemy _enemy)
        {
            _enemy.energy -= AttackPoint;
            Debug.Log(_enemy.name + " is energy " + _enemy.energy);
        }

        protected override void CheckDead()
        {
            base.CheckDead();
            if (energy <= 0)
            {
                Debug.Log("Player is Dead");
            }
        }

    }

}