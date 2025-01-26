using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource;   

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f; 
    [Range(0f, 1f)] public float sfxVolume = 1f;   

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}
