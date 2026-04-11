using UnityEngine;
using UnityEngine.SceneManagement; // Cần thiết để chuyển cảnh
using UnityEngine.UI;
using UnityEngine.InputSystem; // Cần thiết cho hệ thống Input mới của bạn

public class PauseManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Settings UI")]
    public Slider musicSlider;
    public Slider volumeSlider;

    private bool _isPaused = false;

    void Update()
    {
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f; 
        _isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = true;
        _isPaused = false;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
    public void ExitToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("HomeMenu"); 
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    
    // Cần thêm biến này để kéo nguồn nhạc vào
    [Header("Audio References")]
    public AudioSource musicSource; 

    // Trong PauseManager.cs
    public void OnMusicVolumeChange()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(musicSlider.value);
        }
    }

    public void OnSFXVolumeChange()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSfxVolume(volumeSlider.value);
        }
    }
}