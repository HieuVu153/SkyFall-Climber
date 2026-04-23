using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int price;

    [TextArea] public string description;

    public ItemType itemType;
}

public enum ItemType
{
    None,
    Rocket,
    Hook
}