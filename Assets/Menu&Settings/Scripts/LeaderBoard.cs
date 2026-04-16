using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI Elements (Kéo đủ 10 ô vào đây)")]
    public TMP_Text[] nameTexts;   // dùng TMP
    public TMP_Text[] scoreTexts;  // dùng TMP

    private const string LeaderboardKey = "LeaderboardData";

    void Start()
    {
        UpdateLeaderboardUI();
    }

    [System.Serializable]
    public class ScoreEntry
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<ScoreEntry> list = new List<ScoreEntry>();
    }

    public void AddNewScore(string playerName, int newScore)
    {
        LeaderboardData data = LoadData();
        data.list.Add(new ScoreEntry { name = playerName, score = newScore });

        // Sắp xếp giảm dần
        data.list.Sort((x, y) => y.score.CompareTo(x.score));

        // Giữ top 10
        if (data.list.Count > 10)
            data.list.RemoveRange(10, data.list.Count - 10);

        SaveData(data);
        UpdateLeaderboardUI();
    }

    public void UpdateLeaderboardUI()
    {
        LeaderboardData data = LoadData();

        for (int i = 0; i < nameTexts.Length; i++)
        {
            if (i < data.list.Count)
            {
                nameTexts[i].text = (i + 1) + ". " + data.list[i].name;
                scoreTexts[i].text = data.list[i].score.ToString();
            }
            else
            {
                nameTexts[i].text = (i + 1) + ". ---";
                scoreTexts[i].text = "0";
            }
        }
    }

    private void SaveData(LeaderboardData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(LeaderboardKey, json);
        PlayerPrefs.Save(); // thêm dòng này cho chắc
    }

    private LeaderboardData LoadData()
    {
        string json = PlayerPrefs.GetString(LeaderboardKey, "");
        if (string.IsNullOrEmpty(json)) return new LeaderboardData();
        return JsonUtility.FromJson<LeaderboardData>(json);
    }
}