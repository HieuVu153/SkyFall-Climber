using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabSaveManager : MonoBehaviour
{
    public void SaveGameData(Vector3 position, int coins)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "PlayerPosX", position.x.ToString() },
                { "PlayerPosY", position.y.ToString() },
                { "TotalCoins", coins.ToString() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Đã lưu dữ liệu lên PlayFab!"),
            error => Debug.LogError("Lỗi khi lưu: " + error.GenerateErrorReport())
        );
    }
}