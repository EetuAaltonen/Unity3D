using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerClickHandler
{
    private InventorySlot _inventorySlot;
    private InventoryController _inventoryControllerScript;
    private GameObject _itemHolderRef;
    private RotateViewer _viewerScript;

    // Start is called before the first frame update
    void Start()
    { 
        _inventorySlot = GetComponent<InventorySlot>();
        _inventoryControllerScript = _inventorySlot.InventoryController.GetComponent<InventoryController>();
        _itemHolderRef = GameObject.Find("ItemInspectorHolder");
        _viewerScript = _itemHolderRef.GetComponent<RotateViewer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SwapViewerItem(_inventorySlot.Item);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem(_inventorySlot.Item);
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

    public void DropItem(ItemData item)
    {
        _inventoryControllerScript.DropItem(item, 1);
        ClearViewerItem();
    }
}
