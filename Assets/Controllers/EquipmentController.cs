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
        Transform slotBone = GetEquipSlotBone(item.EquipSlot);
        _equippedItems[item.EquipSlot] = item;
        CreateEquipItemInstance(item, slotBone);
    }

    public void UnequipItem(ItemData item)
    {
        Transform slotBone = GetEquipSlotBone(item.EquipSlot);
        _equippedItems[item.EquipSlot] = null;
        DestroyEquipItemInstance(slotBone);
    }

    public void CreateEquipItemInstance(ItemData item, Transform slotBone)
    {
        var equipPosition = slotBone.position;
        var slotObj = Instantiate(
            item.Prefab,
            equipPosition,
            Quaternion.Euler(Vector3.zero)
        );
        slotObj.transform.SetParent(slotBone);
        slotObj.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
        slotObj.name = item.Name;
    }

    public void DestroyEquipItemInstance(Transform slotBone)
    {
        foreach (Transform child in slotBone)
        {
            Destroy(child.gameObject);
        }
    }

    public Transform GetEquipSlotBone(EquipmentSlot equipmentSlot)
    {
        Transform bone = null;

        switch (equipmentSlot)
        {
            case EquipmentSlot.PrimaryWeapon:
                {
                    bone = _player.transform.Find("PlayerModel/RootBone/Hips/Waist/Chest/Arm.R/Forearm.R/Hand.R/Hand.R 1");
                }
                break;
        }
        return bone;
    }
}
