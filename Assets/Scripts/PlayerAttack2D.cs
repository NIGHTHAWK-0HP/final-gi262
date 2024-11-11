using UnityEngine;

public class PlayerAttack2D : MonoBehaviour
{
    public float attackRange = 1.5f;    // ระยะโจมตี
    public int attackDamage = 20;       // ความเสียหายที่ทำ
    public LayerMask enemyLayer;        // เลเยอร์ของศัตรู

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // กดปุ่ม Space เพื่อโจมตี
        {
            Attack();
        }
    }

    void Attack()
    {
        // สร้าง Raycast 2D จากตำแหน่งตัวละครไปในทิศทางที่ตัวละครหันไป
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, enemyLayer);

        if (hit.collider != null) // ถ้า Raycast ตรงกับศัตรู
        {
            // รับความเสียหายจากการโจมตี
            EnemyHealth2D enemy = hit.collider.GetComponent<EnemyHealth2D>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage); // ส่งความเสียหายให้กับศัตรู
                Debug.Log("Attacked enemy for " + attackDamage + " damage");
            }
        }
    }
}