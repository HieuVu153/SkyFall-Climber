using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public TimeManager timeManager;
    public CoinManager coinManager;

    void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        // Load coin từ PlayFab
        coinManager.LoadCoin();

        // Reset time
        timeManager.ResetTime();

        // Bắt đầu đếm thời gian
        timeManager.isPlaying = true;
    }

    public void WinGame()
    {
        timeManager.isPlaying = false;

        int finalTime = timeManager.GetTime();

        // Lưu coin
        coinManager.SaveCoin();

        // Gửi leaderboard
        SendTime(finalTime);

        Debug.Log("WIN - Time: " + finalTime);
    }

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