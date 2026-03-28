using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Default Clips")]
    public AudioClip backgroundMusic;

    void Awake()
    {
        // Giữ AudioManager không bị xóa khi chuyển Scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        // Chỉnh âm lượng tổng của cả game (bao gồm tiếng click nút, tiếng động...)
        AudioListener.volume = volume;
    }
}