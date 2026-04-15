using UnityEngine;
using PlayFab;

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

        if (coinManager == null)
        {
            Debug.LogError("CoinManager bị null!");
            return;
        }

        // ❗ chưa login → không cho ăn
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogWarning("Chưa login → không cộng coin");
            return;
        }

        // ❗ chưa load xong → không cho ăn
        if (!coinManager.isLoaded)
        {
            Debug.LogWarning("Chưa load coin xong!");
            return;
        }

        Coin coin = other.GetComponent<Coin>();
        int value = (coin != null) ? coin.value : 1;

        Debug.Log("Ăn coin: " + value);

        coinManager.AddCoin(value);

        Destroy(other.gameObject);
    }
}