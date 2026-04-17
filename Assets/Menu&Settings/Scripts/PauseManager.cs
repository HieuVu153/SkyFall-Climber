using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Globalization;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject settingsMenu;

    private GameController gameController;
    private bool isSaving = false; // chống spam click

    void Start()
    {
        gameController = Object.FindFirstObjectByType<GameController>();

        if (gameController == null)
        {
            Debug.LogWarning("Không tìm thấy GameController!");
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        DataPersistence.IsContinuing = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ================== EXIT + SAVE ==================
    public void ExitToMenu()
    {
        if (isSaving) return; // tránh bấm nhiều lần
        isSaving = true;

        Time.timeScale = 1f;

        if (gameController == null)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        // Lấy dữ liệu
        Vector3 pos = gameController.player.transform.position;
        int coin = gameController.coinManager.coin;

        // Convert chuẩn (tránh lỗi dấu phẩy)
        string x = pos.x.ToString(CultureInfo.InvariantCulture);
        string y = pos.y.ToString(CultureInfo.InvariantCulture);
        string z = pos.z.ToString(CultureInfo.InvariantCulture);

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Coin", coin.ToString() },
                { "PosX", x },
                { "PosY", y },
                { "PosZ", z }
            }
        };

        // Kiểm tra đã login chưa
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogWarning("Chưa login PlayFab → không lưu!");
            SceneManager.LoadScene("MainMenu");
            return;
        }

        PlayFabClientAPI.UpdateUserData(request,
        result =>
        {
            Debug.Log("Đã lưu trước khi thoát!");
            SceneManager.LoadScene("MainMenu");
        },
        error =>
        {
            Debug.LogError("Lỗi lưu: " + error.GenerateErrorReport());
            SceneManager.LoadScene("MainMenu");
        });
    }

    // ================== SETTINGS ==================
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
}