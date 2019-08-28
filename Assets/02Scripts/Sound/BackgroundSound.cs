using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip english_background_sound;
    public AudioClip math_background_sound;
    public AudioClip basic_background_sound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
                BasicbackGround();
                break;
            case "MathScene":
                MathbackGround();
                break;
            case "EnglishScene":
                EnglishbackGround();
                break;
        }
    }

    public void EnglishbackGround()
    {
        audioSource.clip = english_background_sound;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(audioSource.clip);
        audioSource.loop = true;
    }
    public void MathbackGround()
    {
        audioSource.clip = math_background_sound;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(audioSource.clip);
        audioSource.loop = true;
    }
    public void BasicbackGround()
    {
        audioSource.clip = basic_background_sound;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(audioSource.clip);
        audioSource.loop = true;
    }
}
