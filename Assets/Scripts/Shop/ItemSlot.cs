using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData data; // Kéo file vật phẩm đã tạo ở Bước 1 vào đây
    public Image iconImage;

    void Start()
    {
        if(data != null) iconImage.sprite = data.itemIcon;
    }

    public void OnSlotClick()
    {
        // Khi click vào ô này, nó báo cho ShopManager hiện thông tin lên bảng phải
        FindObjectOfType<ShopManager>().ShowPreview(data);
    }
}