using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    Rigidbody rigidBody;

    public float bulletspeed = 15f;


    public GameObject bulletImpactEffect;

    public AudioClip BulletHitAudio;

    public void InitializeBullet(Vector3 originalDirection)
    {
        transform.forward = originalDirection; 
        rigidBody.velocity = transform.forward * bulletspeed;   

    }
    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play3D(BulletHitAudio, transform.position);

        VFXManager.instance.PlayVFX(bulletImpactEffect, transform.position);
        
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
