using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] songs; // 노래들을 저장할 배열
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomSong();
    }

    void Update()
    {
        if (!audioSource.isPlaying) // 현재 노래가 재생되지 않고 있으면
        {
            PlayRandomSong(); // 새 노래 재생
        }
    }

    void PlayRandomSong()
    {
        if (songs.Length > 0)
        {
            int randomIndex = Random.Range(0, songs.Length); // 무작위 인덱스 선택
            audioSource.clip = songs[randomIndex]; // 선택된 노래를 오디오 소스에 할당
            audioSource.Play(); // 노래 재생
        }
    }
}
