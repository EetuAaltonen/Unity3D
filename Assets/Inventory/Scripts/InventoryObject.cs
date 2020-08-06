using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string SavePath;
    public ItemDatabaseObject Database;
    public Inventory Container;
}

[System.Serializable]
public class Inventory
{
    public List<ItemData> Items = new List<ItemData>();
}
