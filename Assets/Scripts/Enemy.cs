using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float fireRate = 0.75f;
    public GameObject bulletPrefab;
    public GameObject bulletFiringEffect;
    public Transform bulletPosition;
    private float nextFire;
    public AudioClip playerShootingAudio;

    [HideInInspector]
    public int health = 100;
    public Slider healthBar;

    // This triggers when a collider stays within the trigger zone of the enemy
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Using CompareTag for better performance
        {
            transform.LookAt(other.transform); // Rotate enemy towards player
            Fire(); // Call Fire method when player is detected
        }
    }

    // This triggers when a collision stays active
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Corrected the typo
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();

            if (bullet != null)
            {
                // Call the TakeDamage method to decrease health
                TakeDamage(bullet.damage);
            }
        }
    }

    // Method to handle enemy taking damage
    void TakeDamage(int damage)
    {
        health -= damage;
        // Optionally update the health bar here
        if (healthBar != null)
        {
            healthBar.value = health; // Update the health bar slider
        }

        if (health <= 0)
        {
            Die(); // Handle death logic if health reaches 0
        }
    }

    // Method to handle enemy death
    void Die()
    {
        // Handle death, e.g., play death animation, destroy the enemy, etc.
        Destroy(gameObject); // Destroy the enemy game object
    }

    // Method to handle firing bullets
    void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            // Instantiate and initialize the bullet
            if (bulletPrefab != null && bulletPosition != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);

                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    bulletController.InitializeBullet(transform.rotation * Vector3.forward);
                }
                else
                {
                    Debug.LogWarning("BulletController component is missing on the bullet prefab.");
                }
            }
            else
            {
                Debug.LogWarning("Bullet prefab or bullet position is missing on the Enemy script.");
            }

            // Play shooting audio
            if (AudioManager.Instance != null && playerShootingAudio != null)
            {
                AudioManager.Instance.Play3D(playerShootingAudio, transform.position);
            }
            else
            {
                Debug.LogWarning("AudioManager instance or playerShootingAudio is missing.");
            }

            // Play firing visual effect
            if (VFXManager.Instance != null && bulletFiringEffect != null)
            {
                VFXManager.Instance.PlayVFX(bulletFiringEffect, bulletPosition.position);
            }
            else
            {
                Debug.LogWarning("VFXManager instance or bulletFiringEffect is missing.");
            }
        }
    }
}




