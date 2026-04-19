using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI Shop")]
    public GameObject shopPanel;
    public KeyCode openKey = KeyCode.B;

    [Header("Preview Panel")]
    public Image previewIcon;
    public TextMeshProUGUI previewName;
    public TextMeshProUGUI previewDesc;
    public TextMeshProUGUI previewPrice;
    public Button buyButton;

    public CoinManager coinManager;

    private ItemData selectedItem;

    private bool isBuying = false; // ✅ chống spam / gọi 2 lần

    void Start()
    {
        shopPanel.SetActive(false);

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners(); // ✅ tránh bị add 2 lần
            buyButton.onClick.AddListener(BuyItem);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            bool isActive = !shopPanel.activeSelf;
            shopPanel.SetActive(isActive);

            Cursor.visible = isActive;
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void ShowPreview(ItemData data)
    {
        if (data == null) return;

        selectedItem = data;

        previewIcon.sprite = data.itemIcon;
        previewName.text = data.itemName;
        previewDesc.text = data.description;
        previewPrice.text = "Giá: " + data.price;
    }

    public void BuyItem()
    {
        // ✅ chống gọi 2 lần
        if (isBuying) return;
        isBuying = true;

        if (selectedItem == null)
        {
            isBuying = false;
            return;
        }

        if (coinManager == null)
        {
            Debug.LogError("Chưa gắn CoinManager!");
            isBuying = false;
            return;
        }

        int currentCoin = coinManager.GetCoin();

        if (currentCoin >= selectedItem.price)
        {
            coinManager.SetCoin(currentCoin - selectedItem.price);

            Debug.Log("Mua thành công: " + selectedItem.itemName);

            // TODO: buff tại đây
        }
        else
        {
            Debug.Log("Không đủ tiền!");
        }

        isBuying = false;
    }
}