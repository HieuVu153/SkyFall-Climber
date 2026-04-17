using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject victoryPanel;
    public GameObject leaderboardPanel;

    [Header("Audio Settings")]
    public AudioSource musicSource; // Kéo AudioSource (nhạc nền) từ SettingsManager qua đây
    public AudioClip victoryMusic;  // Kéo file nhạc chiến thắng vào đây

    void Start()
    {
        // Khi Panel Victory hiện lên, ta đổi nhạc
        if (victoryPanel.activeSelf)
        {
            ChangeToVictoryMusic();
        }
    }

    public void ChangeToVictoryMusic()
    {
        if (musicSource != null && victoryMusic != null)
        {
            // Kiểm tra nếu nhạc đang phát không phải là nhạc chiến thắng thì mới đổi
            if (musicSource.clip != victoryMusic)
            {
                musicSource.Stop();
                musicSource.clip = victoryMusic;
                musicSource.loop = true; // Thường nhạc victory sẽ lặp lại trong lúc chờ
                musicSource.Play();
            }
        }
    }

    public void ToggleLeaderboard(bool show)
    {
        leaderboardPanel.SetActive(show);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DemoPause");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DemoUI"); 
    }
}