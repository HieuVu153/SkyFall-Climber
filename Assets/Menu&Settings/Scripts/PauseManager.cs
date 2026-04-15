using UnityEngine;
using UnityEngine.SceneManagement; // Bắt buộc có dòng này để load lại cảnh

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;      
    public GameObject pauseButton;    
    public GameObject settingsMenu; 

    // 1. Hàm Resume (Tiếp tục)
    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f; 
    }

    // 2. Hàm Pause (Tạm dừng)
    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f; 
    }

    // 3.  Play Again (Chơi lại)
    public void PlayAgain()
    {
        Time.timeScale = 1f; // QUAN TRỌNG: Phải cho thời gian chạy lại trước khi load
        // Load lại chính Scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenSettingsInPause()
    {
        pauseMenu.SetActive(false);    // Ẩn bảng Pause
        settingsMenu.SetActive(true); // Hiện bảng Settings (cái bảng mà bạn đã làm ở Main Menu)
    }

    public void CloseSettingsInPause()
    {
        settingsMenu.SetActive(false); // Ẩn bảng Settings
        pauseMenu.SetActive(true);    // Hiện lại bảng Pause để người chơi chọn tiếp
    }

    // 4. Thoát về Menu chính
    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DemoUI"); // Thay bằng tên Scene Menu của bạn
    }
    
}