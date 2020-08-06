using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public string SavePath;
    public ItemDatabaseObject Database;
    public InventoryObject InventoryObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void AddItem(ItemData item, int amount)
    {
        // TODO: Items with buffs are non-stackable
        /*if (_item.buffs.Length > 0)
        {
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            return;
        }*/
        var _item = new ItemData(item);
        _item.Amount = amount;
        var index = GetSlotIndex(_item);
        if (index == InventoryObject.Container.Items.Count)
        {
            InventoryObject.Container.Items.Add(_item);
        }
        else
        {
            if (_item.Id == InventoryObject.Container.Items[index].Id)
            {
                InventoryObject.Container.Items[index].AddAmount(_item.Amount);
            }
            else
            {
                InventoryObject.Container.Items.Insert(index, _item);
            }
        }
    }

    private void OnApplicationQuit()
    {
        InventoryObject.Container.Items.Clear();
    }

    public int GetSlotIndex(ItemData item)
    {
        int i;
        var inventorySize = InventoryObject.Container.Items.Count;
        for (i = 0; i < inventorySize; i++)
        {
            var _item = InventoryObject.Container.Items[i];
            if (_item.Id >= item.Id) break;
        }
        return i;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //Editable by player
        /*string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();*/

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, InventoryObject);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, SavePath)))
        {
            //Editable by player
            /*BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();*/

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, SavePath), FileMode.Open, FileAccess.Read);
            InventoryObject = (InventoryObject)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        InventoryObject = new InventoryObject();
    }
}
