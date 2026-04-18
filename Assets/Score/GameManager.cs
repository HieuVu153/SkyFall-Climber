using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0; // Đây chính là tiền của bạn
    public TMP_Text scoreText;

    private void Awake()
    {
        // Singleton để các script khác (như Shop) có thể gọi tới dễ dàng
        instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    // Hàm này dùng khi nhặt được coin trên map
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // --- HÀM QUAN TRỌNG CHO SHOP ---
    // Shop sẽ gọi hàm này để kiểm tra xem bạn có đủ tiền mua không
    public bool SpendScore(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            UpdateScoreUI(); // Cập nhật lại chữ hiển thị tiền trên màn hình
            return true; // Trả về "Đúng" -> Cho phép mua
        }
        return false; // Trả về "Sai" -> Không đủ tiền
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}