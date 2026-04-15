using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject settingsMenu;

    // Tham chiếu đến các script chứa dữ liệu (tùy bạn để coin/pos ở đâu)
    // Ở đây tôi ví dụ gọi qua GameController để lưu nhanh
    private GameController gameController;

    void Start()
    {
        gameController = Object.FindFirstObjectByType<GameController>();
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
        // Đánh dấu để GameController biết là cần load lại vị trí cũ
        DataPersistence.IsContinuing = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- SỬA HÀM NÀY ---
    public void ExitToMenu()
    {
        Time.timeScale = 1f; // Trả lại thời gian trước khi lưu/chuyển scene

        if (gameController != null)
        {
            // Thực hiện lưu dữ liệu cuối cùng trước khi thoát
            // Chúng ta dùng một Dictionary để lưu nhanh trực tiếp ở đây
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "Coin", gameController.coinManager.coin.ToString() },
                    { "PosX", gameController.player.transform.position.x.ToString() },
                    { "PosY", gameController.player.transform.position.y.ToString() },
                    { "PosZ", gameController.player.transform.position.z.ToString() }
                }
            };

            // Lưu xong mới chuyển Scene
            PlayFabClientAPI.UpdateUserData(request, result => {
                Debug.Log("Đã lưu trước khi thoát!");
                SceneManager.LoadScene("MainMenu");
            }, error => {
                Debug.LogError("Lỗi lưu khi thoát: " + error.GenerateErrorReport());
                SceneManager.LoadScene("MainMenu"); // Lỗi vẫn cho thoát nhưng báo lỗi
            });
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    // -------------------

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