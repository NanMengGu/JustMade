using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public int delayInMilliseconds;
    public float pitch = 1f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = pitch; // pitch 설정
        PlayAudioWithDelay(delayInMilliseconds);
    }


    void PlayAudioWithDelay(int delayMilliseconds)
    {
        audioSource.time = delayMilliseconds / 1000f;
        audioSource.Play();
    }
}
