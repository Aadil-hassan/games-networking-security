using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fireRate = 0.75f;
    public GameObject bulletPrefab;
    public GameObject bulletFiringEffect;
    public Transform bulletPosition;
    private float nextFire;
    public AudioClip playerShootingAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Using CompareTag for better performance
        {
            transform.LookAt(other.transform);
            Fire();
        }
    }

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



