using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damage = 10;
    private Transform player; // 儲存玩家的位置
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 透過標籤找到玩家物件，並獲取它的 Transform 元件
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player == null) return; // 如果玩家不存在了，就停止移動

        // 計算從敵人指向玩家的方向
        Vector2 direction = (player.position - transform.position).normalized;
        // 讓敵人朝那個方向移動
        rb.velocity = direction * moveSpeed;

        // (可選) 讓敵人也轉向玩家
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰到的是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // 對玩家造成傷害
            }
        }
    }
}