using UnityEngine;
using UnityEngine.UI;

public sealed class SettingManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // Nhạc nền
    [SerializeField] private AudioSource sfxSource;   // Âm thanh game

    private void Start()
    {
        // Setup slider Music
        if (musicSlider != null)
        {
            musicSlider.value = 1f;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // Setup slider Sound
        if (soundSlider != null)
        {
            soundSlider.value = 1f;
            soundSlider.onValueChanged.AddListener(SetSoundVolume);
        }
    }

    // 🎵 Điều chỉnh nhạc nền
    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }
    }

    // 🔊 Điều chỉnh âm thanh game
    public void SetSoundVolume(float value)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = value;
        }
    }

    // Mở Setting
    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    // Đóng Setting
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
}