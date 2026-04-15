using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private CoinManager coinManager;

    void Start()
    {
        coinManager = FindFirstObjectByType<CoinManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();

            int value = (coin != null) ? coin.value : 10;

            coinManager.AddCoin(value);

            Destroy(other.gameObject);
        }
    }
}