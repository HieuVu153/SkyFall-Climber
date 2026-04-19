using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop/Item")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int price;
    [TextArea] public string description;
}