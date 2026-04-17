using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincessTrigger : MonoBehaviour
{
    private bool playerInRange = false;

    [Header("UI")]
    public GameObject pressEUI;

    [Header("Player")]
    public Transform player;

    private GameController gameController;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();

        // Ẩn text lúc đầu
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        // 👉 Công chúa luôn nhìn player
        LookAtPlayer();

        // 👉 Nhấn E để win
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            WinGame();
        }
    }

    // ================== LOOK AT PLAYER ==================
    void LookAtPlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x)
        {
            // Player bên phải → quay phải
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // Player bên trái → quay trái
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // ================== TRIGGER ==================
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pressEUI != null)
                pressEUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pressEUI != null)
                pressEUI.SetActive(false);
        }
    }

    // ================== WIN GAME ==================
    void WinGame()
    {
        Debug.Log("WIN GAME!");

        // 👉 Save + Leaderboard
        if (gameController != null)
        {
            gameController.WinGame();
        }

        // 👉 Load scene thắng
        SceneManager.LoadScene("WinGame");
    }
}