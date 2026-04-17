using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coin;

    // ================== ADD ==================
    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log("Coin hiện tại: " + coin);
    }

    // ================== GET ==================
    public int GetCoin()
    {
        return coin;
    }

    // ================== SET (khi load từ PlayFab) ==================
    public void SetCoin(int value)
    {
        coin = value;
        Debug.Log("Set coin: " + coin);
    }
}