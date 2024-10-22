using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingScript : MonoBehaviour
{
    public Transform PlayerCharacter;

    Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - PlayerCharacter.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerCharacter.position + cameraOffset;    
    }
}
