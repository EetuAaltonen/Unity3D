using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public static EquipmentController Instance;

    public delegate void OnEquipmentChanged(ItemData newItem, ItemData oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private Dictionary<EquipmentSlot, ItemData> _equippedItems = new Dictionary<EquipmentSlot, ItemData>();

    private GameObject _player;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _player = CharacterUtilities.FindCharacterPlayer();

        _equippedItems = InitEquippedItems();
    }

    public Dictionary<EquipmentSlot, ItemData> InitEquippedItems()
    {
        Dictionary<EquipmentSlot, ItemData> equippedItems = new Dictionary<EquipmentSlot, ItemData>();

        foreach (EquipmentSlot equipmentSlot in (EquipmentSlot[])System.Enum.GetValues(typeof(EquipmentSlot)))
        {
            equippedItems.Add(equipmentSlot, null);
        }

        return equippedItems;
    }

    public void ToggleEquipItem(ItemData item)
    {
        var currentEquipment = _equippedItems[item.EquipSlot];

        // Trigger onEquipmentChanged
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(item, currentEquipment);
        }

        if (currentEquipment == null)
        {
            // Equip new item
            EquipItem(item);
        } else
        {
            if (ItemDataUtilities.CompareItems(currentEquipment, item))
            {
                // Unequip current item when data is the same
                UnequipItem(item);
            } else
            {
                EquipItem(item);
            } 
        }
    }

    public void EquipItem(ItemData item)
    {
        _equippedItems[item.EquipSlot] = item;
        CreateEquipItemInstance(item);
    }

    public void UnequipItem(ItemData item)
    {
        _equippedItems[item.EquipSlot] = null;
        DestroyEquipItemInstance(item.EquipSlot);
    }

    public void CreateEquipItemInstance(ItemData item)
    {
        Transform equipSlot = GetEquipSlotTransform(item.EquipSlot);
        Vector3 equipPosition = equipSlot.position;
        Vector3 equipPositionOffset = InstancePrefabUtilities.GetEquipPositionOffset(item.InstancePrefab);

        GameObject itemInstance = Instantiate(
            item.InstancePrefab,
            equipPosition,
            Quaternion.Euler(Vector3.zero)
        );
        itemInstance.transform.SetParent(equipSlot);
        itemInstance.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        itemInstance.transform.localPosition -= equipPositionOffset;
        itemInstance.name = item.Name;

        InstancePrefabUtilities.DisableItemInstancePhysics(itemInstance);
    }

    public void DestroyEquipItemInstance(EquipmentSlot equipmentSlot)
    {
        Transform equipSlot = GetEquipSlotTransform(equipmentSlot);
        foreach (Transform child in equipSlot)
        {
            Destroy(child.gameObject);
        }
    }

    public Transform GetEquipSlotTransform(EquipmentSlot equipmentSlot)
    {
        string equipSlotName = Enum.GetName(typeof(EquipmentSlot), equipmentSlot) + "Slot";
        return TransformChildUtilities.FindChildTransformRecursive(_player.transform, equipSlotName);
    }
}
