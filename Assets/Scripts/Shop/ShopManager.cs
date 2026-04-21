using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public KeyCode openKey = KeyCode.B;

    public HotbarManager hotbarManager;
    public CoinManager coinManager;

    public Image previewIcon;
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
        if (isBuying) return;
        isBuying = true;

        if (selectedItem == null || coinManager == null || hotbarManager == null)
        {
            isBuying = false;
            return;
        }

        int currentCoin = coinManager.GetCoin();

        if (currentCoin >= selectedItem.price)
        {
            coinManager.SetCoin(currentCoin - selectedItem.price);
            hotbarManager.AddItem(selectedItem);

            Debug.Log("Mua thành công: " + selectedItem.itemName);
        }
        else
        {
            Debug.Log("Không đủ tiền!");
        }

        isBuying = false;
    }
}