using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject shopPanel;
    public KeyCode openKey = KeyCode.B;

    [Header("Manager")]
    public HotbarManager hotbarManager;
    public CoinManager coinManager;

    [Header("Preview")]
    public Image previewIcon; // ✅ FIX: chỉ cần 1 ảnh
    public TextMeshProUGUI previewName;
    public TextMeshProUGUI previewDesc;
    public TextMeshProUGUI previewPrice;
    public Button buyButton;

    private ItemData selectedItem;
    private bool isBuying = false;

    void Start()
    {
        shopPanel.SetActive(false);

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(BuyItem);
        }

        if (hotbarManager == null)
            hotbarManager = FindFirstObjectByType<HotbarManager>();

        if (coinManager == null)
            coinManager = FindFirstObjectByType<CoinManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            ToggleShop();
        }
    }

    void ToggleShop()
    {
        bool isActive = !shopPanel.activeSelf;
        shopPanel.SetActive(isActive);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowPreview(ItemData data)
    {
        if (data == null) return;

        selectedItem = data;

        // ✅ FIX: không còn array
        if (previewIcon != null)
            previewIcon.sprite = data.itemIcon;

        if (previewName != null)
            previewName.text = data.itemName;

        if (previewDesc != null)
            previewDesc.text = data.description;

        if (previewPrice != null)
            previewPrice.text = "Giá: " + data.price;
    }

    public void BuyItem()
    {
        if (isBuying) return;
        isBuying = true;

        if (selectedItem == null)
        {
            Debug.LogWarning("Chưa chọn item!");
            isBuying = false;
            return;
        }

        if (coinManager == null || hotbarManager == null)
        {
            Debug.LogError("Thiếu Manager!");
            isBuying = false;
            return;
        }

        int currentCoin = coinManager.GetCoin();

        // ❌ KHÔNG ĐỦ TIỀN
        if (currentCoin < selectedItem.price)
        {
            Debug.Log("Không đủ tiền!");
            isBuying = false;
            return;
        }

        // ❌ SLOT ĐANG CHỌN ĐÃ CÓ ITEM
        if (!IsCurrentSlotEmpty())
        {
            Debug.Log("Slot đang chọn đã có item!");
            isBuying = false;
            return;
        }

        // ✅ MUA
        coinManager.SetCoin(currentCoin - selectedItem.price);

        bool added = hotbarManager.AddItemToCurrentSlot(selectedItem);

        if (!added)
        {
            isBuying = false;
            return;
        }

        // 🔥 HOOK +1 LẦN DÙNG
        PlayerHook hook = FindFirstObjectByType<PlayerHook>();
        if (hook != null && selectedItem.itemName == "Hook")
        {
            hook.hookCount += 1;
        }

        Debug.Log("Mua thành công: " + selectedItem.itemName);

        isBuying = false;
    }

    bool IsCurrentSlotEmpty()
    {
        return hotbarManager.items[hotbarManager.GetCurrentIndex()] == null;
    }
}