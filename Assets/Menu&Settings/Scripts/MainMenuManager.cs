using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu; // Kéo Panel Settings vào đây

    // Hàm cho nút "Chơi Tiếp"
    public void ContinueGame() {
        // Lấy level đã lưu, nếu chưa có thì mặc định là level 1
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        SceneManager.LoadScene(savedLevel);
    }

    // Hàm cho nút "Chơi Mới"
    public void PlayGame() {
        // Chơi mới thường là load Scene đầu tiên sau MainMenu (Index 1)
        SceneManager.LoadScene(1);
    }

    public void OpenSettings() {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false); // Ẩn Menu chính
    }

    public void ExitGame() {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}