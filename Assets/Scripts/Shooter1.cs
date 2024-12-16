using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioClip shootSound;
    public AudioSource audioSource;

    public float shootCooldown = 1f;
    private float cooldownTimer = 0f;

    public float shootInterval = 3f; // Time interval between shots
    private float nextShootTime = 0f;

    public string playerTag = "Player"; // Tag for the player

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Time.time >= nextShootTime && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag); // Find the player tagged with "Player"

        if (player != null)
        {
            Vector3 direction = (player.transform.position - firePoint.position).normalized;
            
            var bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(Vector3.forward, direction));
            var bullet = bulletGO.GetComponent<Bullet1>(); // Adjust reference to Bullet1
            bullet.Initialize(player.transform); // Pass player transform to the bullet
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}