using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;
    private Rigidbody rigidBody;

    public float fireRate = 0.75f;
    public GameObject bulletPrefab;
    public GameObject bulletFiringEffect;
    public Transform bulletPosition;
    private float nextFireTime;

    public AudioClip playerShootingAudio;

    // Initialize components
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Rigidbody component missing on Player!");
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

            bullet.GetComponent<BulletController>()?.InitializeBullet(transform.rotation * Vector3.forward);
            AudioManager.Instance.Play3D(playerShootingAudio, transform.position);

            VFXManager.Instance.PlayVFX(bulletFiringEffect, bulletPosition.position);
        }
    }
}

