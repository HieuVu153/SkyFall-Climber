using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public TimerManager timerManager; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng chạm vào là Nhân vật
        if (collision.CompareTag("Player")) 
        {
            timerManager.StopTimer();
        }
    }
}