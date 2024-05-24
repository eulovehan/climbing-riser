using System.Collections;
using UnityEngine;

public class Intro1Manager : MonoBehaviour
{
    public AudioClip sirenSound;
    private AudioSource audioSource;

    void Start()
    {
        // 오디오 소스 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1.0f;
        audioSource.clip = sirenSound;

        // 사이렌 소리 재생
        PlaySirenSound();
    }

    public void PlaySirenSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Siren sound is not set.");
        }
    }

    public void StopSirenSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
