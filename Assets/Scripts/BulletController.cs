using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float bulletSpeed = 15f;
    public GameObject bulletImpactEffect;
    public AudioClip bulletHitAudio;  // Bullet hit audio

    public void InitializeBullet(Vector3 originalDirection)
    {
        transform.forward = originalDirection;  // Set bullet direction
        rigidBody.velocity = transform.forward * bulletSpeed;  // Move bullet
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure bulletHitAudio is assigned before playing
        if (bulletHitAudio != null)
        {
            AudioManager.Instance.Play3D(bulletHitAudio, transform.position);  // Play hit sound
        }
        else
        {
            Debug.LogWarning("bulletHitAudio not assigned in the Inspector for BulletController.");
        }

        // Play impact effect
        if (bulletImpactEffect != null)
        {
            VFXManager.Instance.PlayVFX(bulletImpactEffect, transform.position);
        }

        Destroy(gameObject);  // Destroy the bullet after collision
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();  // Initialize the rigid body component
    }
}



