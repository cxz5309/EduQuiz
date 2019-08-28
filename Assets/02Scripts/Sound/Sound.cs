using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    public AudioClip correct;
    public AudioClip incorrect;
    public AudioClip bbong;
    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    public void Correct()
    {
        audioSource.clip = correct;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void InCorrect()
    {
        audioSource.clip = incorrect;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void shoot_sound(){
        audioSource.clip = bbong;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioSource.clip);
    }
    
}
