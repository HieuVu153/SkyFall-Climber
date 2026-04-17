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

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        InitGame();
    }

    void InitGame()
    {
        // Không còn load vị trí hay data nữa
        Debug.Log("Start Game Mới");
    }

    // ================== WIN GAME ==================
    public void WinGame()
    {
        timeManager.isPlaying = false;
        int finalTime = timeManager.GetTime();

        // Lưu coin (nếu bạn vẫn dùng hàm này nội bộ)
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