using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _playMusic;
    public static AudioManager Instance;
    private void Start()
    {
        Instance = this;
    }
    public void PlayMusic()
    {
        _playMusic.Play();
    }
    public void StopMusic()
    {
        _playMusic.Stop();
    }
}
