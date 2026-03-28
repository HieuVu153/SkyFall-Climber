using UnityEngine;
using UnityEngine.SceneManagement; // Để chuyển cảnh

public class HomeMenuManager : MonoBehaviour
{
    [Header("Cấu hình Panels")]
    public GameObject settingsPanel; // Kéo bảng Setting vào đây

    // 1. Hàm cho nút PLAY
    public void PlayGame()
    {
        // Chuyển sang Scene tiếp theo hoặc tên Scene chơi game của bạn
        // Ví dụ: SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 2. Hàm cho nút MENU (Mở Setting)
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // 3. Hàm cho nút EXIT
    public void ExitGame()
    {
        Debug.Log("Game đã thoát!"); 
        Application.Quit(); // Thoát game
    }
}