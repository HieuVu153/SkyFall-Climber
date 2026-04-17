using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI Elements (Kéo đủ 10 ô vào đây)")]
    public TMP_Text[] nameTexts;
    public TMP_Text[] timeTexts;

    // ================== LOAD LEADERBOARD ==================
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "BestTime",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
    }

    // ================== HIỂN THỊ ==================
    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        for (int i = 0; i < nameTexts.Length; i++)
        {
            if (i < result.Leaderboard.Count)
            {
                var item = result.Leaderboard[i];

                int realTime = item.StatValue; // ✅ FIX

                string name = string.IsNullOrEmpty(item.DisplayName)
                    ? item.PlayFabId
                    : item.DisplayName;

                nameTexts[i].text = (i + 1) + ". " + name;
                timeTexts[i].text = FormatTime(realTime);
            }
            else
            {
                nameTexts[i].text = (i + 1) + ". ---";
                timeTexts[i].text = "--:--:--";
            }
        }
    }

    // ================== FORMAT TIME ==================
    string FormatTime(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }

    // ================== ERROR ==================
    void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());

        for (int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i].text = (i + 1) + ". ---";
            timeTexts[i].text = "--:--:--";
        }
    }
}