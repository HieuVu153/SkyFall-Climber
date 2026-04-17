using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Giao diện")]
    public GameObject shopPanel;      // Kéo cái Panel Shop vào
    public KeyCode openKey = KeyCode.B; // Phím mở shop

    [Header("Bảng chi tiết bên phải")]
    public Image previewIcon;
    public TextMeshProUGUI previewName, previewDesc, previewPrice;
    public Button buyButton;

    private ItemData selectedItem; // Món đồ đang chọn

    void Start()
    {
        shopPanel.SetActive(false); // Lúc đầu ẩn shop đi
    }

    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            bool isActive = !shopPanel.activeSelf;
            shopPanel.SetActive(isActive);
            
            // Hiện chuột để bấm Shop
            Cursor.visible = isActive;
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    // Hàm hiển thị thông tin khi click vào 1 ô vật phẩm
    public void ShowPreview(ItemData data)
    {
        selectedItem = data;
        previewIcon.sprite = data.itemIcon;
        previewName.text = data.itemName;
        previewDesc.text = data.description;
        previewPrice.text = "Giá: " + data.price;
    }

    // Hàm bấm nút Mua
    public void BuyItem()
    {
        if (selectedItem != null)
        {
            // GỌI ĐẾN GAMEMANAGER ĐỂ TRỪ TIỀN
            if (GameManager.instance.SpendScore(selectedItem.price))
            {
                Debug.Log("Mua thành công món: " + selectedItem.itemName);
                // Bạn có thể thêm code cộng chỉ số nhân vật ở đây
            }
            else
            {
                Debug.Log("Bạn không đủ tiền!");
            }
        }
    }
}