using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;

    Rigidbody rigidBody;

    public float firerate = 0.75f;
    public GameObject bulletPrefab;
    public Transform bulletPosition;
    float nextfire;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle firing in the Update loop
        if (Input.GetKey(KeyCode.Space))
            Fire();
    }

    // FixedUpdate is called for physics updates like movement
    void FixedUpdate()
    {
        Move();
    }

    // Movement logic
    void Move()
    {
        // Get player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // If there's no input, do not rotate or move
        if (horizontalInput == 0 && verticalInput == 0)
            return;

        // Calculate movement direction
        Vector3 movementDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Smooth rotation towards movement direction
        if (movementDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);  // Smooth rotation
        }

        // Move the player based on input
        rigidBody.MovePosition(rigidBody.position + movementDir * movementSpeed * Time.deltaTime);
    }

    // Firing logic
    void Fire()
    {
        // Ensures firing happens only after the specified firerate interval
        if (Time.time > nextfire)
        {
            nextfire = Time.time + firerate;

            // Instantiate bullet at the defined position with no rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);

            bullet.GetComponent<BulletController>()?.InitializeBullet(transform.rotation * Vector3.forward);
        }
    }
}
