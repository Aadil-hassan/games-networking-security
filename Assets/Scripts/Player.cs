using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;
    private Rigidbody rigidBody;

    public float fireRate = 0.75f;
    public GameObject bulletPrefab;
    public GameObject bulletFiringEffect;
    public Transform bulletPosition;
    private float nextFireTime;

    [HideInInspector]
    public int health = 100;
    public Slider healthBar;

    public AudioClip playerShootingAudio;

    // Initialize components
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Rigidbody component missing on Player!");
        }

        if (healthBar != null)
        {
            healthBar.maxValue = health;
            healthBar.value = health;
        }
    }

    // Handle firing in the Update loop
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }

    // FixedUpdate is called for physics updates like movement
    void FixedUpdate()
    {
        Move();
    }

    // Handle collisions with bullets and decrease health
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletController bullet = collision.gameObject.GetComponent<BulletController>();

            if (bullet != null)
            {
                // Call the TakeDamage method to decrease health
                TakeDamage(bullet.damage);
            }
        }
    }

    // Method to handle player taking damage
    void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBar != null)
        {
            healthBar.value = health; // Update the health bar slider
        }

        if (health <= 0)
        {
            Die(); // Handle death logic if health reaches 0
        }
    }

    // Method to handle player death
    void Die()
    {
        // Handle death, e.g., play death animation, destroy the player, etc.
        Debug.Log("Player has died.");
        Destroy(gameObject); // Destroy the player game object
    }

    // Movement logic
    private void Move()
    {
        // Get player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // If there's no input, skip movement and rotation
        if (horizontalInput == 0 && verticalInput == 0)
            return;

        // Calculate movement direction
        Vector3 movementDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Smooth rotation towards movement direction
        if (movementDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
        }

        // Move the player based on input
        rigidBody.MovePosition(rigidBody.position + movementDir * movementSpeed * Time.deltaTime);
    }

    // Firing logic
    private void Fire()
    {
        // Ensures firing happens only after the specified fireRate interval
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Instantiate bullet at the defined position with no rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);

            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.InitializeBullet(transform.rotation * Vector3.forward);
            }
            else
            {
                Debug.LogError("BulletController component missing on the bullet prefab.");
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



