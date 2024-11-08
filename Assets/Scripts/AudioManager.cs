using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // Singleton pattern for easy access
    public GameObject AudioPrefab;  // AudioPrefab for instantiating audio

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Ensure this persists across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicates
        }
    }

    // Method to play 3D audio at a given position
    public void Play3D(AudioClip clip, Vector3 position)
    {
        GameObject audioGameObject = Instantiate(AudioPrefab, position, Quaternion.identity);
        AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(audioGameObject, clip.length);  // Destroy the audio game object after the clip finishes playing
    }
}



