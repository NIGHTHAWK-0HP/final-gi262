using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Student
{
    public class OOPItemPotion : Identity
    {
        public int healPoint = 10;
        public bool isBonus;

        private void Start()
        {
            // 20% chance to be a bonus potion
            isBonus = Random.Range(0, 100) < 20;
            
            // Visual feedback for bonus potions
            if (isBonus)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

        public override void Hit()
        {
            if (mapGenerator == null || mapGenerator.player == null)
            {
                Debug.LogError("MapGenerator or Player not set. Unable to heal.");
                return;
            }

            if (isBonus)
            {
                mapGenerator.player.Heal(healPoint, isBonus);
                Debug.Log("You got " + Name + " Bonus: " + healPoint * 2);
            }
            else
            {
                mapGenerator.player.Heal(healPoint);
                Debug.Log("You got " + Name + ": " + healPoint);
            }

            // Set mapdata to 0 to remove the potion
            if (positionX >= 0 && positionX < mapGenerator.X && positionY >= 0 && positionY < mapGenerator.Y)
            {
                mapGenerator.mapdata[positionX, positionY] = 0;
            }
            Destroy(gameObject);
        }

        // Optional: Add interaction with the player using a trigger
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Hit();  // Call the Hit method when the player collides with the potion
            }
        }
    }
}