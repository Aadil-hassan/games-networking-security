using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fireRate = 0.75f;
    public GameObject bulletPrefab;
    public Transform bulletPosition;
    float nextFire;
    public AudioClip playerShootingAudio;
    public GameObject bulletFiringEffect;

    private void OnTriggerEneter(Collider other)
    {   
        if (other.gameObject.Tag.Equals("Player"))
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
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
            bullet.GetComponent<BulletController>()?.InitializeBullet(transform.rotation * Vector3.forward);
            AudioManager.Instance.Play3D(playerShootingAudio, transform.position);

            VFXManager,Instance.PlayVFX(bulletFiringEffect, bulletPosition,position);

        }







    }



}
