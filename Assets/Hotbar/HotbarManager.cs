using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public Image[] slots;
    public ItemData[] items;

    private int currentIndex = 0;

    void Awake()
    {
        // ✅ luôn đồng bộ size → tránh crash
        items = new ItemData[slots.Length];
    }

    void Update()
    {
        // chọn slot
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectSlot(6);

        // dùng item
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
    }

    public void AddItem(ItemData item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                slots[i].sprite = item.itemIcon;
                slots[i].color = Color.white;
                return;
            }
        }

        Debug.Log("Hotbar đầy!");
    }

    void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Length) return;

        currentIndex = index;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].color = (i == index) ? Color.yellow : Color.white;
        }
    }

    void UseItem()
    {
        if (currentIndex >= items.Length) return;

        ItemData item = items[currentIndex];

        if (item == null)
        {
            Debug.Log("Slot trống");
            return;
        }

        switch (item.itemType)
        {
            case ItemType.Rocket:
                UseRocket();
                break;
        }

        // ✅ DÙNG XONG → XÓA KHỎI HOTBAR
        items[currentIndex] = null;
        slots[currentIndex].sprite = null;
        slots[currentIndex].color = new Color(1, 1, 1, 0); // ẩn
    }

    void UseRocket()
    {
        PlayerRocket rocket = FindFirstObjectByType<PlayerRocket>();

        if (rocket != null)
        {
            rocket.ActivateRocket(30f, 2f);
            Debug.Log("🚀 Rocket activated");
        }
        else
        {
            Debug.LogError("Không tìm thấy PlayerRocket!");
        }
    }
}