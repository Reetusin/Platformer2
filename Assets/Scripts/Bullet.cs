using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float speed = 20;
    public Vector2 damageRange = new Vector2(10, 20);
    public float lifetime = 3;

    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var damage = Random.Range(damageRange.x, damageRange.y);

        //TODO: apply damage to health component
        //TODO: damage indictor

        print($"Dealt {damage} damage to {other.gameObject.name}");
        Destroy(gameObject);
    }
}
