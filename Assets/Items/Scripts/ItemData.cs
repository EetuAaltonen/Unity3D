using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemData : ScriptableObject
{
    public int Id;
    public GameObject InstancePrefab;
    public string Name;
    public ItemType Type;
    public EquipmentSlot EquipSlot;
    public float Weight;
    public int Amount;
    public List<StatModifier> StatModifier;
    [TextArea(15, 20)]
    public string Description;
    public void Init(int id, GameObject instancePrefab, string name, ItemType type, EquipmentSlot equipSlot, float weight, int amount, List<StatModifier> itemBuffs, string description)
    {
        Id = id;
        InstancePrefab = instancePrefab;
        Name = name;
        Type = type;
        EquipSlot = equipSlot;
        Weight = weight;
        Amount = amount;
        StatModifier = itemBuffs;
        FetchItemBuffSource();
        Description = description;
    }

    public void Init(ItemData item)
    {
        Id = item.Id;
        InstancePrefab = item.InstancePrefab;
        Name = item.Name;
        Type = item.Type;
        EquipSlot = item.EquipSlot;
        Weight = item.Weight;
        Amount = item.Amount;
        StatModifier = item.StatModifier;
        FetchItemBuffSource();
        Description = item.Description;
    }

    public void FetchItemBuffSource()
    {
        foreach (StatModifier itemBuff in StatModifier)
        {
            itemBuff.Source = Name;
        }
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}
