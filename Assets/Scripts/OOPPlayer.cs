using UnityEngine;

public class OOPPlayer : Character
{
    public float moveSpeed = 5f;

    private void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.H))  // Heal with "H" key for testing
        {
            Heal(20);  // Heal by 20 points
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}