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

            // If it's a bonus potion, double the healing amount
            if (isBonus)
            {
                mapGenerator.player.Heal(healPoint * 2);  // Pass only one argument (healing amount)
                Debug.Log("You got " + Name + " Bonus: " + healPoint * 2);
            }
            else
            {
                mapGenerator.player.Heal(healPoint);  // Regular healing
                Debug.Log("You got " + Name + ": " + healPoint);
            }

            // Set mapdata to 0 to remove the potion from the map
            if (positionX >= 0 && positionX < mapGenerator.X && positionY >= 0 && positionY < mapGenerator.Y)
            {
                mapGenerator.mapdata[positionX, positionY] = 0;
            }

            // Destroy the potion object
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