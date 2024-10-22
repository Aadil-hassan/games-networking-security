using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject AudioPrefab;
    public static AudioManager Instance;

    private void Awake()
    {
        // Implementing Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist the AudioManager across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy the duplicate instance of the AudioManager
        }
    }

    // Method to play 3D audio
    public void Play3D(AudioClip clip, Vector3 position)
    {
        // Instantiate the audio object at the specified position
        GameObject audioGameObject = Instantiate(AudioPrefab, position, Quaternion.identity);
        AudioSource source = audioGameObject.GetComponent<AudioSource>();

        source.clip = clip;
        source.Play();

        Destroy(audioGameObject, clip.length);
        
    }
}

