using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public float speedIncreaseAmount = 2f; // Amount to increase the player's speed
    public float speedDuration = 5f;       // How long the speed increase lasts

    // When the player collides with the item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OOPPlayer player = other.GetComponent<OOPPlayer>();
            if (player != null)
            {
                player.IncreaseSpeed(speedIncreaseAmount, speedDuration); // Increase player's speed
                Destroy(gameObject);  // Destroy the item after it's collected
            }
        }
    }
}

