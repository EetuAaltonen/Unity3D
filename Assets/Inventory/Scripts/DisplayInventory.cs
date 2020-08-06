using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Microsoft.Unity.VisualStudio.Editor;

public class DisplayInventory : MonoBehaviour
{
    public GameObject InventoryPrefab;
    public InventoryObject Inventory;
    public Transform InventorySlotArea;
    public int YStartPos;
    public int YMargin;
    public int SlotCount;
    public int StartIndex;
    
    private bool _refreshInventory;

    // Update is called once per frame
    void Update()
    {
        if (_refreshInventory)
        {
            _refreshInventory = false;
            UpdateInventorySlots();
        }
    }

    public void RequestInventoryRefresh()
    {
        _refreshInventory = true;
    }

    private void CreateInventorySlots()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.name = InventoryPrefab.name;
            obj.transform.SetParent(InventorySlotArea);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    private void UpdateInventorySlots()
    {
        if (InventorySlotArea.transform.Find(InventoryPrefab.name) == null) CreateInventorySlots();

        var slots = GetInventorySlotObjects();
        var i = 0;
        foreach (var slot in slots)
        {
            var item = i >= Inventory.Container.Items.Count ? null : Inventory.Container.Items[i];
            UpdateInventorySlot(slot.gameObject, item);
            i++;
        }
    }

    private List<Transform> GetInventorySlotObjects()
    {
        List<Transform> slots = new List<Transform>();
        foreach (Transform child in InventorySlotArea.transform)
        {
            if (child.name == InventoryPrefab.name)
            {
                slots.Add(child);
            }
        }
        return slots;
    }

    private void UpdateInventorySlot(GameObject slot, ItemData item)
    {
        slot.GetComponent<InventorySlot>().Item = item;
        TextMeshProUGUI[] children = slot.GetComponentsInChildren<TextMeshProUGUI>();
        children[0].text = item != null ? item.Name : "";
        children[1].text = item != null ? item.Amount.ToString("n0") : "";    
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(0f, YStartPos + (-YMargin * i), 0f);
    }
}
