using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu; // Kéo Panel Settings vào đây

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings() {
        settingsMenu.SetActive(true);
        this.gameObject.SetActive(false); // Ẩn Menu chính
    }

    public void ExitGame() {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}