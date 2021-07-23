using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private InventoryController _inventoryController;
    private EquipmentController _equipmentController;


    private void Start()
    {
        _inventoryController = InventoryController.Instance;
        _equipmentController = EquipmentController.Instance;
        _equipmentController.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(ItemData newItem, ItemData oldItem)
    {

    }
}