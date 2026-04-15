using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public int coin;

    public void AddCoin(int amount)
    {
        coin += amount;
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
            {"Coin", coin.ToString()}
        }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Save Coin OK"),
            error => Debug.LogError(error.GenerateErrorReport()));
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
                    coin = int.Parse(result.Data["Coin"].Value);
                }
                else
                {
                    coin = 0;
                }

                Debug.Log("Coin: " + coin);
            },
            error => Debug.LogError(error.GenerateErrorReport()));
    }
}