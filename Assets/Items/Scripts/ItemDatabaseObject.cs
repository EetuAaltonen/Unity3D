using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemData[] WeaponData;
    public ItemData[] EquipmentData;
    public ItemData[] FoodData;
    public ItemData[] PotionData;
    public ItemData[] BookData;
    public ItemData[] QuestData;
    public ItemData[] GeneralData;

    public Dictionary<int, ItemData> ItemDictionary = new Dictionary<int, ItemData>();

    public ItemData[][] GetAllItemData()
    {
        return new ItemData[][]
        {
            WeaponData,
            EquipmentData,
            FoodData,
            PotionData,
            BookData,
            QuestData,
            GeneralData
        };
    }

    public void OnAfterDeserialize()
    {
        AddItemsToDictionary();
    }

    public void OnBeforeSerialize()
    {
        CreateItemDictionary();
    }

    [ContextMenu("SortItems")]
    public void SortItems()
    {
        CreateItemDictionary();

        var allItemData = GetAllItemData();
        foreach (var itemCategory in allItemData)
        {
            if (itemCategory.Length > 0)
            {
                Array.Sort(itemCategory, delegate (ItemData item1, ItemData item2)
                {
                    return item1.Name.CompareTo(item2.Name);
                });
            }
        }

        AddItemsToDictionary();
    }

    private void CreateItemDictionary()
    {
        ItemDictionary = new Dictionary<int, ItemData>();
    }

    private void AddItemsToDictionary()
    {
        var id = 0;
        var allItemData = GetAllItemData();
        foreach (var itemCategory in allItemData)
        {
            foreach (var item in itemCategory)
            {
                item.Id = id;
                ItemDictionary.Add(id, item);
                id++;
            }
        }
    }
}
