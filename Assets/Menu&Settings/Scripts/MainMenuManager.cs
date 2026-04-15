using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;

    // CHƠI MỚI (Reset toàn bộ)
    public void NewGame()
    {
        DataPersistence.IsContinuing = false; // Đánh dấu là chơi mới
        DataPersistence.TargetPosition = null;

        // Reset dữ liệu trên PlayFab về mặc định
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Coin", "0" },
                { "PosX", "0" },
                { "PosY", "1" },
                { "PosZ", "0.48" } // Giữ Z mặc định của bạn
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            },
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    // CHƠI TIẾP (Tải dữ liệu trước khi vào game)
    public void PlayGame()
    {
        // 1. Gọi PlayFab lấy dữ liệu
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("PosX"))
            {
                // 2. Lưu vào lớp trung gian
                float x = float.Parse(result.Data["PosX"].Value);
                float y = float.Parse(result.Data["PosY"].Value);
                float z = float.Parse(result.Data["PosZ"].Value);
                int coins = int.Parse(result.Data["Coin"].Value);

                DataPersistence.TargetPosition = new Vector3(x, y, z);
                DataPersistence.TargetCoins = coins;
                DataPersistence.IsContinuing = true;

                // 3. Chuyển scene sau khi đã có dữ liệu
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                // Nếu chưa có dữ liệu bao giờ thì cho chơi mới luôn
                NewGame();
            }
        }, error => {
            Debug.LogError("Lỗi tải dữ liệu: " + error.GenerateErrorReport());
            // Nếu lỗi mạng, có thể vẫn cho vào game nhưng ở vị trí mặc định
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}