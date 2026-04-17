using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject victoryPanel;
    public GameObject leaderboardPanel;

    [Header("Leaderboard")]
    public LeaderboardManager leaderboardManager;

    [Header("Audio Settings")]
    public AudioSource musicSource;
    public AudioClip victoryMusic;

    void Start()
    {
        if (victoryPanel.activeSelf)
        {
            ChangeToVictoryMusic();
        }
    }

    public void ChangeToVictoryMusic()
    {
        if (musicSource != null && victoryMusic != null)
        {
            if (musicSource.clip != victoryMusic)
            {
                musicSource.Stop();
                musicSource.clip = victoryMusic;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }

    // ================== MỞ / TẮT BXH ==================
    public void ToggleLeaderboard(bool show)
    {
        leaderboardPanel.SetActive(show);

        if (show && leaderboardManager != null)
        {
            Invoke(nameof(LoadBoard), 0.5f); // ✅ FIX delay
        }
    }

    void LoadBoard()
    {
        leaderboardManager.GetLeaderboard();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}