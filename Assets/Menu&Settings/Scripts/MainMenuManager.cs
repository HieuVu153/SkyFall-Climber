using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Globalization;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;

    // ================== CHƠI MỚI ==================
    public void NewGame()
    {
        DataPersistence.IsContinuing = false;
        DataPersistence.TargetPosition = null;

        var data = new Dictionary<string, string>();

        // Lưu vị trí spawn chuẩn (KHÔNG hardcode sai)
        Vector3 spawn = new Vector3(0f, 1f, 0.48f);

        data["PosX"] = spawn.x.ToString(CultureInfo.InvariantCulture);
        data["PosY"] = spawn.y.ToString(CultureInfo.InvariantCulture);
        data["PosZ"] = spawn.z.ToString(CultureInfo.InvariantCulture);
        data["Coin"] = "0";

        var request = new UpdateUserDataRequest
        {
            Data = data
        };

        PlayFabClientAPI.UpdateUserData(request,
            result =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        );
    }

    // ================== CHƠI TIẾP ==================
    public void PlayGame()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        result =>
        {
            float x, y, z;
            int coins;

            bool ok =
                TryGetFloat(result, "PosX", out x) &&
                TryGetFloat(result, "PosY", out y) &&
                TryGetFloat(result, "PosZ", out z) &&
                TryGetInt(result, "Coin", out coins);

            if (ok)
            {
                Debug.Log($"Load Pos: {x}, {y}, {z}");

                DataPersistence.TargetPosition = new Vector3(x, y, z);
                DataPersistence.TargetCoins = coins;
                DataPersistence.IsContinuing = true;
            }
            else
            {
                Debug.LogError("Data PlayFab lỗi → không thể Continue");

                DataPersistence.IsContinuing = false;
                DataPersistence.TargetPosition = null;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        },
        error =>
        {
            Debug.LogError("Lỗi tải dữ liệu: " + error.GenerateErrorReport());

            DataPersistence.IsContinuing = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    // ================== TRY GET (KHÔNG DEFAULT) ==================
    bool TryGetFloat(GetUserDataResult result, string key, out float value)
    {
        value = 0f;

        if (result.Data == null || !result.Data.ContainsKey(key))
        {
            Debug.LogError($"Thiếu key: {key}");
            return false;
        }

        string raw = result.Data[key].Value;

        if (string.IsNullOrEmpty(raw))
        {
            Debug.LogError($"Key rỗng: {key}");
            return false;
        }

        if (!float.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
        {
            Debug.LogError($"Parse lỗi {key}: {raw}");
            return false;
        }

        return true;
    }

    bool TryGetInt(GetUserDataResult result, string key, out int value)
    {
        value = 0;

        if (result.Data == null || !result.Data.ContainsKey(key))
        {
            Debug.LogError($"Thiếu key: {key}");
            return false;
        }

        string raw = result.Data[key].Value;

        if (string.IsNullOrEmpty(raw))
        {
            Debug.LogError($"Key rỗng: {key}");
            return false;
        }

        if (!int.TryParse(raw, out value))
        {
            Debug.LogError($"Parse lỗi {key}: {raw}");
            return false;
        }

        return true;
    }

    // ================== UI ==================
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