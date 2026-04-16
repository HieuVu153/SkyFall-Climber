using UnityEngine;
using TMPro; // Nếu dùng TextMeshPro

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning = false;

    void Start()
    {
        // Bắt đầu tính giờ ngay khi load cảnh
        isRunning = true;
        elapsedTime = 0f;
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            DisplayTime(elapsedTime);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Định dạng phút và giây (00:00)
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
        // Bạn có thể thêm logic hiện bảng tổng kết điểm tại đây
        Debug.Log("Thời gian hoàn thành: " + timerText.text);
    }
}