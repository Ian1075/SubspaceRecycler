using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;

    private float nextFireTime;
    private Transform target;

    void Start()
    {
    }

    void Update()
    {
        if (target == null)
        {
            // 就重新尋找一個帶有 "Enemy" 標籤的物件當作新目標
            GameObject newTarget = GameObject.FindWithTag("Enemy");
            if (newTarget != null)
            {
                target = newTarget.transform;
            }
            else
            {
                // 如果找不到任何敵人，就返回
                return;
            }
        }

        Vector2 directionToTarget = (target.position - transform.position).normalized;
        firePoint.up = directionToTarget;

        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}