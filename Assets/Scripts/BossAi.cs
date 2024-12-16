using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    public GameObject objectToDisable;
    public int maxHits = 20;

    private float fireCooldown = 0f;
    private int currentHits = 0;

    void Update()
    {
        FollowPlayer();
        LookAtPlayer();
        ShootAtPlayer();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.z = 0; 
            transform.up = direction; 
        }
    }

    void ShootAtPlayer()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f && player != null)
        {
            FireBullet();
            fireCooldown = fireRate;
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            currentHits++;
            Destroy(other.gameObject);

            if (currentHits >= maxHits)
            {
                if (objectToDisable != null)
                {
                    objectToDisable.SetActive(false); // Disable specific object
                }
                Destroy(gameObject); // Destroy the boss
            }
        }
    }
}