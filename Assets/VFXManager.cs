using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void PlayVFX(GameObject effectObject, Vector3 effectPosition)
    {
        GameObject vfxObject = Instantiate(effectObject , effectPosition, Quaternion.identity);

        ParticleSystem[] particleSystems = vfxObject.GetComponentsInChildren<ParticleSystem>();

        float MaxLength = 0f;
        foreach (ParticleSystem individualParticleSystem in particleSystems)
        {
            float currentKnownMaxLength = individualParticleSystem.main.duration
                + individualParticleSystem.main.startLifetime.constantMax;

            if (currentKnownMaxLength > MaxLength)
                MaxLength = currentKnownMaxLength;
        }

        Destroy(vfxObject, MaxLength);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
