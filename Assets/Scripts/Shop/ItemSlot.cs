using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("Data")]
    public ItemData data;

    [Header("UI")]
    public Image iconImage;

    private ShopManager shopManager;

    void Start()
    {
        shopManager = FindFirstObjectByType<ShopManager>();

        if (data != null && iconImage != null)
        {
            iconImage.sprite = data.itemIcon;
        }
    }

    public void OnSlotClick()
    {
        if (data != null && shopManager != null)
        {
            shopManager.ShowPreview(data);
        }
    }
}