using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private CoinManager coinManager;

    void Start()
    {
        coinManager = FindFirstObjectByType<CoinManager>();

        if (coinManager == null)
        {
            Debug.LogError("Không tìm thấy CoinManager trong scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Coin")) return;

        if (coinManager == null) return;

        Coin coin = other.GetComponent<Coin>();
        int value = (coin != null) ? coin.value : 1;

        coinManager.AddCoin(value);

        Destroy(other.gameObject);
    }
}