using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource _backgroundMusic;

    private void Start()
    {
        Instance = this;
        _backgroundMusic = GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        _backgroundMusic.Play();
    }
    public void StopMusic()
    {
        _backgroundMusic.Stop();
    }
}
