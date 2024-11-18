using UnityEngine;

public class EnemyAI : Character
{
    public float moveSpeed = 3f;
    public Transform player;

    private void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}