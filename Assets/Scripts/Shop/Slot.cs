using UnityEngine;

public class Slot : MonoBehaviour {
    public ItemData data; // Kéo file Kiếm hoặc Máu vào đây
    public void ClickSlot() {
        FindObjectOfType<ShopManager>().ShowPreview(data);
    }
}
