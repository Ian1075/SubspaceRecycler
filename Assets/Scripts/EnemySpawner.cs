// EnemySpawner.cs
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // 敵人的預製件
    [SerializeField] private float spawnRate = 2f;   // 每幾秒生成一個
    [SerializeField] private float spawnRadius = 5f; // 在生成點周圍多大的半徑內隨機生成

    private float nextSpawnTime = 0;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // 在一個圓形範圍內計算一個隨機位置
        Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        
        // 生成敵人
        Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        Debug.Log("生成一個新敵人！");
    }
}