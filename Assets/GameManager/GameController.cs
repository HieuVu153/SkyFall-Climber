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
        yield return new WaitForSeconds(1f);

        if (GameData.isContinue)
        {
            LoadGame();
        }
        else
        {
            StartNewGame();
        }

        StartCoroutine(AutoSave());
    }

    void StartNewGame()
    {
        timeManager.ResetTime();
        coinManager.SetCoin(0);

        player.transform.position = Vector3.zero;

        timeManager.StartTimer();

        Debug.Log("New Game");
    }

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

                int coin = result.Data.ContainsKey("Coin") ? int.Parse(result.Data["Coin"].Value) : 0;
                int time = result.Data.ContainsKey("Time") ? int.Parse(result.Data["Time"].Value) : 0;

                player.transform.position = new Vector3(x, y, z);
                coinManager.SetCoin(coin);
                timeManager.SetTime(time);

                timeManager.StartTimer();

                Debug.Log("Continue Game");
            }
            else
            {
                StartNewGame();
            }
        },
        error =>
        {
            Debug.LogError("Load lỗi: " + error.GenerateErrorReport());
            StartNewGame();
        });
    }

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

    IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SaveGame();
        }
    }

    // ================== WIN GAME ==================
    public void WinGame(System.Action onDone = null)
    {
        timeManager.StopTimer();

        int finalTime = timeManager.GetTime();

        Debug.Log("WIN - Time: " + finalTime);

        SaveGame();

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "BestTime",
                    Value = finalTime
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result =>
            {
                Debug.Log("Send Time OK");
                onDone?.Invoke(); // ✅ gửi xong mới chuyển scene
            },
            error =>
            {
                Debug.LogError(error.GenerateErrorReport());
                onDone?.Invoke();
            });
    }
}