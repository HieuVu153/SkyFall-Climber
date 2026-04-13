using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public TMP_Text scoreText;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score; // <-- dòng 19 lỗi thường ở đây
    }
}