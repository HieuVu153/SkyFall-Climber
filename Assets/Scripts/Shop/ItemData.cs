using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;      // Tên món đồ
    public Sprite itemIcon;      // Hình ảnh món đồ
    public int price;            // Giá tiền
    [TextArea] public string description; // Mô tả món đồ
}