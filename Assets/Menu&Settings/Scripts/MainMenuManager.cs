using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu; // Panel Settings

    // ================== CONTINUE ==================
    public void ContinueGame()
    {
        GameData.isContinue = true;
        SceneManager.LoadScene("Map"); // scene game của bạn
    }

    // ================== NEW GAME ==================
    public void NewGame()
    {
        GameData.isContinue = false;
        SceneManager.LoadScene("Map");
    }

    // ================== SETTINGS ==================
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        gameObject.SetActive(true);
    }

    // ================== EXIT ==================
    public void ExitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}