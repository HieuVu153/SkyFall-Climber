using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioSource musicSource; // Kéo MusicManager vào đây
    public AudioSource sfxSource;   // Kéo SFXManager vào đây
    public GameObject mainMenu;
    public GameObject settingsMenu;

    // Chỉnh nhạc nền
    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
            Debug.Log("Am luong hien tai: " + value); // Dong nay se hien o cua so Console
        }
        else
        {
            Debug.LogError("Chua keo AudioSource vao SettingsManager roi ban oi!");
        }
    }

    // Chỉnh âm thanh game (SFX)
    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}