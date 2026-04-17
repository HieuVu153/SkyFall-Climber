using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TimeManager timeManager;
    public CoinManager coinManager;
    public GameObject player; // Kéo thả Player vào đây trong Inspector
    private Coroutine saveCoroutine;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); // đợi PlayFab ổn định
        InitGame();
    }

    void InitGame()
    {
        if (DataPersistence.IsContinuing && DataPersistence.TargetPosition != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            Vector3 savedPos = DataPersistence.TargetPosition.Value;
            savedPos.z = 0.48f; // Giữ Z chuẩn của bạn

            if (rb != null)
            {
                rb.simulated = false;      // Tắt vật lý tạm thời
                rb.linearVelocity = Vector2.zero; // Xóa vận tốc cũ
                rb.position = savedPos;    // Gán trực tiếp vào Rigidbody
                player.transform.position = savedPos; // Gán vào Transform
            }

            // Ép Unity cập nhật hệ thống ngay lập tức
            Physics2D.SyncTransforms();

            if (rb != null) rb.simulated = true; // Bật lại vật lý

            coinManager.coin = DataPersistence.TargetCoins;
            DataPersistence.IsContinuing = false;

            Debug.Log("ĐÃ ÉP VỊ TRÍ THÀNH CÔNG");
        }
    }
    public void AutoSavePosition()
    {
        if (saveCoroutine != null) StopCoroutine(saveCoroutine);
        saveCoroutine = StartCoroutine(SaveAfterDelay());
    }

    IEnumerator SaveAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 pos = player.transform.position;

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
        {
            { "Coin", coinManager.coin.ToString() },
            { "PosX", pos.x.ToString(System.Globalization.CultureInfo.InvariantCulture) },
            { "PosY", pos.y.ToString(System.Globalization.CultureInfo.InvariantCulture) },
            { "PosZ", pos.z.ToString(System.Globalization.CultureInfo.InvariantCulture) }
        }
        };

        Debug.Log("ĐANG SAVE: " + pos);

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("AutoSave Cloud OK!"),
            error => Debug.LogWarning("AutoSave lỗi: " + error.GenerateErrorReport())
        );
    }

    public void WinGame()
    {
        timeManager.isPlaying = false;
        int finalTime = timeManager.GetTime();

        // Lưu coin cuối cùng
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