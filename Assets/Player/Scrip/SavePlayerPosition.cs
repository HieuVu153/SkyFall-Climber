using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;

public class SavePlayerPosition : MonoBehaviour
{
    public Transform player;

    public void SavePosition()
    {
        string pos = player.position.x + "," + player.position.y + "," + player.position.z;

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "PlayerPos", pos }
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Lưu vị trí thành công"),
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }
}