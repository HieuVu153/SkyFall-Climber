using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public Image[] slots;
    public ItemData[] items;

    private int currentIndex = 0;

    void Awake()
    {
        items = new ItemData[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = null;
            slots[i].color = new Color(1, 1, 1, 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectSlot(6);

        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
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

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public bool AddItemToCurrentSlot(ItemData item)
    {
        if (currentIndex < 0 || currentIndex >= items.Length) return false;

        if (items[currentIndex] != null)
        {
            Debug.Log("Slot đang chọn đã có item!");
            return false;
        }

        items[currentIndex] = item;
        slots[currentIndex].sprite = item.itemIcon;
        slots[currentIndex].color = Color.white;

        return true;
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

            case ItemType.Hook:
                UseHook();
                break;
        }

        // xóa sau khi dùng
        items[currentIndex] = null;
        slots[currentIndex].sprite = null;
        slots[currentIndex].color = new Color(1, 1, 1, 0);
    }

    void UseRocket()
    {
        PlayerRocket rocket = FindFirstObjectByType<PlayerRocket>();

        if (rocket != null)
        {
            rocket.ActivateRocket(20f, 0.5f);
            Debug.Log("🚀 Rocket dùng");
        }
        else
        {
            Debug.LogError("Không tìm thấy PlayerRocket!");
        }
    }

    void UseHook()
    {
        PlayerHook hook = FindFirstObjectByType<PlayerHook>();

        if (hook != null)
        {
            hook.FireHook();
            Debug.Log("🪝 Hook dùng");
        }
        else
        {
            Debug.LogError("Không tìm thấy PlayerHook!");
        }
    }
}