using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopingSFXSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = .5f; 
    [Range(0f, 1f)] public float sfxVolume = .5f;   

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
        loopingSFXSource.volume = sfxVolume;
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

    public void StopSFX()
    {
        sfxSource.Stop();
        sfxSource.clip = null;
    }

    public void PlayLoopingSFX(AudioClip clip)
    {
        if (loopingSFXSource.clip == clip && loopingSFXSource.isPlaying) return; 
        loopingSFXSource.clip = clip;
        loopingSFXSource.loop = true;
        loopingSFXSource.Play();
    }

    public void StopLoopingSFX()
    {
        loopingSFXSource.Stop();
        loopingSFXSource.clip = null;
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
