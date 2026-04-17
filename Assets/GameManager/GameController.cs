using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TimeManager timeManager;
    public CoinManager coinManager;
    public GameObject player;

    // ================== START ==================
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); // đợi PlayFab login

        if (GameData.isContinue)
        {
            LoadGame();
        }
        else
        {
            StartNewGame();
        }

        StartCoroutine(AutoSave()); // auto save
    }

    // ================== NEW GAME ==================
    void StartNewGame()
    {
        timeManager.ResetTime();
        coinManager.SetCoin(0);

        player.transform.position = Vector3.zero; // hoặc spawn point

        Debug.Log("New Game");
    }

    // ================== LOAD GAME ==================
    void LoadGame()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        result =>
        {
            if (result.Data != null && result.Data.ContainsKey("PosX"))
            {
                float x = float.Parse(result.Data["PosX"].Value);
                float y = float.Parse(result.Data["PosY"].Value);
                float z = float.Parse(result.Data["PosZ"].Value);

                int coin = int.Parse(result.Data["Coin"].Value);
                int time = int.Parse(result.Data["Time"].Value);

                player.transform.position = new Vector3(x, y, z);
                coinManager.SetCoin(coin);
                timeManager.SetTime(time);

                timeManager.StartTimer();

                Debug.Log("Continue Game");
            }
            else
            {
                Debug.Log("Không có data → chuyển sang New Game");
                StartNewGame();
            }
        },
        error =>
        {
            Debug.LogError("Load lỗi: " + error.GenerateErrorReport());
            StartNewGame();
        });
    }

    // ================== SAVE GAME ==================
    public void SaveGame()
    {
        Vector3 pos = player.transform.position;

        var data = new Dictionary<string, string>()
        {
            { "PosX", pos.x.ToString() },
            { "PosY", pos.y.ToString() },
            { "PosZ", pos.z.ToString() },
            { "Coin", coinManager.GetCoin().ToString() },
            { "Time", timeManager.GetTime().ToString() }
        };

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = data
        },
        result => Debug.Log("Save OK"),
        error => Debug.LogError("Save lỗi: " + error.GenerateErrorReport()));
    }

    // ================== AUTO SAVE ==================
    IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SaveGame();
        }
    }

    // ================== WIN GAME ==================
    public void WinGame()
    {
        timeManager.StopTimer();

        int finalTime = timeManager.GetTime();

        SaveGame();
        SendTime(finalTime);

        Debug.Log("WIN - Time: " + finalTime);
    }

    // ================== LEADERBOARD ==================
    void SendTime(int time)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "BestTime",
                    Value = time
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log("Send Time OK"),
            error => Debug.LogError(error.GenerateErrorReport()));
    }
}