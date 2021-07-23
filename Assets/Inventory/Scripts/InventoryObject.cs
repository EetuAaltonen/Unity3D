using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string SavePath;
    public ItemDatabaseObject Database;
    public Inventory Container;
    public float WeightCapacity;
    public float WeightCapacityBuff;
    public float WeightLoad;
}

[System.Serializable]
public class Inventory
{
    public List<ItemData> Items = new List<ItemData>();
}
