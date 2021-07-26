using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public string SavePath;
    public ItemDatabaseObject Database;
    public InventoryObject InventoryObject;

    private const float MIN_DROP_DISTANCE = 2f;
    private const float MAX_DROP_DISTANCE = 4f;
    [SerializeField] private GameObject _collectableGroup;
    [SerializeField] private GameObject _playerInstanceRef;
    [SerializeField] private GameObject _inventoryScreenRef;
    private DisplayInventory _displayScript;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _displayScript = _inventoryScreenRef.GetComponent<DisplayInventory>();
    }

    public void AddItem(ItemData item, int amount)
    {
        var _item = ScriptableObject.CreateInstance(typeof(ItemData)) as ItemData;
        _item.Init(item);
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
        InventoryObject.WeightLoad += (item.Weight * amount);
        _displayScript.RequestInventoryRefresh();
    }

    public void DropItem(ItemData item, int amount)
    {
        var dropPosition = _playerInstanceRef.transform.position;
        dropPosition += _playerInstanceRef.transform.forward.normalized * Random.Range(MIN_DROP_DISTANCE, MAX_DROP_DISTANCE);

        var itemInstance = Instantiate(
            item.InstancePrefab,
            dropPosition,
            Quaternion.Euler(0f, -90f, 90f)
        );
        itemInstance.transform.SetParent(_collectableGroup.transform);
        itemInstance.name = item.InstancePrefab.name;
        itemInstance.GetComponent<Outline>().ReInitOutline();

        RemoveItem(item, 1);
    }

    public void RemoveItem(ItemData item, int amount)
    {
        var index = GetSlotIndex(item);
        if (index == InventoryObject.Container.Items.Count)
        {
            Debug.LogError($"No items found with index {index}");
        }
        else
        {
            var _item = InventoryObject.Container.Items[index];
            if (item.Id == _item.Id)
            {
                if (_item.Amount - amount <= 0)
                {
                    InventoryObject.Container.Items.RemoveAt(index);
                }
                else
                {
                    InventoryObject.Container.Items[index].AddAmount(-amount);
                }
            }
        }
        InventoryObject.WeightLoad += (item.Weight * (-amount));
        _displayScript.RequestInventoryRefresh();
    }

    public int GetSlotIndex(ItemData item)
    {
        int i;
        var inventorySize = InventoryObject.Container.Items.Count;
        for (i = 0; i < inventorySize; i++)
        {
            var _item = InventoryObject.Container.Items[i];
            if (_item.Id > item.Id) return i;
            if (ItemDataUtilities.CompareItems(item, _item)) break;
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
        _displayScript.RequestInventoryRefresh();
    }

    private void OnApplicationQuit()
    {
        InventoryObject.Container.Items.Clear();
    }
}
