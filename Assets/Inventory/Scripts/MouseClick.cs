using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private InventorySlot _inventorySlot;
    private GameObject _itemHolderRef;
    private RotateViewer _viewerScript;

    // Start is called before the first frame update
    void Start()
    {
        _inventorySlot = GetComponent<InventorySlot>();
        _itemHolderRef = GameObject.Find("ItemInspectorHolder");
        _viewerScript = _itemHolderRef.GetComponent<RotateViewer>();
    }

    public void SwapItem()
    {
        _viewerScript.Item = _inventorySlot.Item;
        _viewerScript.SwapItemRequest = true;
    }
}
