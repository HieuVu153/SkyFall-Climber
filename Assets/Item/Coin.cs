using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public string coinID; // ID riêng cho từng coin

    private void Start()
    {
        // nếu coin đã ăn rồi thì xoá khi load game
        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        PlayerPrefs.SetInt(coinID, 1); // đánh dấu đã ăn
        PlayerPrefs.Save();

        Destroy(gameObject);
    }
}