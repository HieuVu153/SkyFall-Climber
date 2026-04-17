using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coin;

    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log("Coin hiện tại: " + coin);
    }

    public int GetCoin()
    {
        return coin;
    }

    public void SetCoin(int value)
    {
        coin = value;
        Debug.Log("Set coin: " + coin);
    }
}