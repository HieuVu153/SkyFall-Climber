using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject settingsMenu;

    private GameController gameController;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
    }

    // 1. Resume
    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    // 2. Pause
    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    // 3. Play Again
    public void PlayAgain()
    {
        Time.timeScale = 1f;

        // 👉 reset game
        GameData.isContinue = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSettingsInPause()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsInPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    // 4. Exit (THÊM SAVE)
    public void ExitToMenu()
    {
        Time.timeScale = 1f;

        // 👉 CHỈ THÊM ĐOẠN NÀY
        if (gameController != null)
        {
            gameController.SaveGame();
        }

        // 👉 để Continue hoạt động
        GameData.isContinue = true;

        SceneManager.LoadScene("MainMenu");
    }
}