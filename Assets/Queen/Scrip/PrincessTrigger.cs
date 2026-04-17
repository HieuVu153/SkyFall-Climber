using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincessTrigger : MonoBehaviour
{
    private bool playerInRange = false;

    public GameObject pressEUI;
    public Transform player;

    private GameController gameController;

    void Start()
    {
        gameController = FindFirstObjectByType<GameController>();

        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        LookAtPlayer();

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            WinGame(); // ✅ gọi thẳng luôn
        }
    }

    void LookAtPlayer()
    {
        if (player == null) return;

        int dir = (player.position.x > transform.position.x) ? 1 : -1;

        transform.localScale = new Vector3(dir, 1, 1);

        FixUIText(dir);
    }

    void FixUIText(int dir)
    {
        if (pressEUI == null) return;

        Vector3 scale = pressEUI.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        pressEUI.transform.localScale = scale;
    }

    void WinGame()
    {
        gameController.WinGame(() =>
        {
            SceneManager.LoadScene("LeaderBoard");
        });
    }

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
}