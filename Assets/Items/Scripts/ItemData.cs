using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemData : ScriptableObject
{
    public int Id;
    public GameObject Prefab;
    public string Name;
    public ItemType Type;
    public float Weight;
    public int Amount;
    public List<ItemBuff> ItemBuffs;
    [TextArea(15, 20)]
    public string Description;
    public void Init(int id, GameObject prefab, string name, ItemType type, float weight, int amount, List<ItemBuff> itemBuffs, string description)
    {
        Id = id;
        Prefab = prefab;
        Name = name;
        Type = type;
        Weight = weight;
        Amount = amount;
        ItemBuffs = itemBuffs;
        Description = description;
    }

    public void Init(ItemData item)
    {
        Id = item.Id;
        Prefab = item.Prefab;
        Name = item.Name;
        Type = item.Type;
        Weight = item.Weight;
        Amount = item.Amount;
        ItemBuffs = item.ItemBuffs;
        Description = item.Description;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}

public enum ItemType
{
    Weapon,
    Equipment,
    Food,
    Potion,
    Book,
    Quest,
    General
}
