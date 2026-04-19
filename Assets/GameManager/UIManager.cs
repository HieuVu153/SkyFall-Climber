using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI coinText;

    public TimeManager timeManager;
    public CoinManager coinManager;

    void Update()
    {
        if (timeManager != null)
        {
            int t = timeManager.GetTime();
            timeText.text = "Time: " + FormatTime(t); // ✅ giống leaderboard
        }

        if (coinManager != null)
        {
            coinText.text = "Coin: " + coinManager.GetCoin();
        }
    }

    // ✅ DÙNG CHUNG FORMAT với Leaderboard
    string FormatTime(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}