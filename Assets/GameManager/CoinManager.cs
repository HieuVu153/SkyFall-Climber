using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public int coin;
    public bool isLoaded = false; // 🔥 kiểm tra đã load xong chưa

    // 👉 GỌI HÀM NÀY SAU KHI LOGIN THÀNH CÔNG
    public void InitAfterLogin()
    {
        LoadCoin();
    }

    public void AddCoin(int amount)
    {
        if (!isLoaded)
        {
            Debug.LogWarning("Chưa load coin xong!");
            return;
        }

        coin += amount;

        Debug.Log("Coin hiện tại: " + coin);

        SaveCoin();
    }

    public void SaveCoin()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogError("Chưa login mà gọi SaveCoin!");
            return;
        }

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Coin", coin.ToString() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Save Coin OK"),
            error => Debug.LogError("Save lỗi: " + error.GenerateErrorReport())
        );
    }

    public void LoadCoin()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogError("Chưa login mà gọi LoadCoin!");
            return;
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            result =>
            {
                if (result.Data != null && result.Data.ContainsKey("Coin"))
                {
                    int.TryParse(result.Data["Coin"].Value, out coin);
                }
                else
                {
                    coin = 0;
                }

                isLoaded = true; // 🔥 QUAN TRỌNG

                Debug.Log("Load coin: " + coin);
            },
            error => Debug.LogError("Load lỗi: " + error.GenerateErrorReport())
        );
    }
}