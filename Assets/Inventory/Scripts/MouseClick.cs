using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerClickHandler
{
    private InventorySlot _inventorySlot;
    private GameObject _itemHolderRef;
    private RotateViewer _viewerScript;

    private InventoryController _inventoryController;
    private EquipmentController _equipmentController;

    // Start is called before the first frame update
    void Start()
    {
        _inventoryController = InventoryController.Instance;
        _equipmentController = EquipmentController.Instance;

        _inventorySlot = GetComponent<InventorySlot>();
        _itemHolderRef = GameObject.Find("ItemInspectorHolder");
        _viewerScript = _itemHolderRef.GetComponent<RotateViewer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // TODO: Shift key not recognized
        if (Input.GetKeyDown(KeyCode.LeftShift) && eventData.button == PointerEventData.InputButton.Left)
        {
            DropItem(_inventorySlot.Item);
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            SwapViewerItem(_inventorySlot.Item);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleEquipItem(_inventorySlot.Item);
        }
    }

    public void SwapViewerItem(ItemData item)
    {
        _viewerScript.Item = item;
        _viewerScript.SwapItemRequest = true;
    }

    public void ClearViewerItem()
    {
        _viewerScript.Item = null;
        _viewerScript.SwapItemRequest = true;
    }

    public void ToggleEquipItem(ItemData item)
    {
        _equipmentController.ToggleEquipItem(item);
    }

    public void DropItem(ItemData item)
    {
        _inventoryController.DropItem(item, 1);
        ClearViewerItem();
    }
}
