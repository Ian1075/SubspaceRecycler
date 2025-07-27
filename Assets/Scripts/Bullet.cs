using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private int damage = 2;

    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    // Bullet.cs - 修改 OnTriggerEnter2D 函式
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 嘗試從碰到的物件上獲取 Health 元件
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null) // 確保對方有血量系統
            {
                enemyHealth.TakeDamage(damage); // 對它造成 damage 點傷害
            }
            Destroy(gameObject); // 子彈自己消失
        }
    }
}