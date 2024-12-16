using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 5f; // Bullet speed
    
    public GameObject hitEffect;
    
    public Vector2 damageRange = new Vector2(10, 20);
    private Transform target; // Reference to the player or target to follow

    public void Initialize(Transform target)
    {
        this.target = target; // Initialize with the target transform
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // If the target is no longer present (destroyed or moved), destroy the bullet
            Destroy(gameObject);
        }

        // Destroy the bullet if it goes off-screen
        if (IsOffScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsOffScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        return !(viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var damage = Random.Range(damageRange.x, damageRange.y);

        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage((int)damage);
        }

        //print("Hit " + other.gameObject.name + " for " + damage + " damage");

        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}