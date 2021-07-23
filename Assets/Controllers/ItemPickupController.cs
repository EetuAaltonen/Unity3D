using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemPickupController : MonoBehaviour
{
    [SerializeField] private GameObject _selectionController;
    [SerializeField] private GameObject _inventoryController;
    private InventoryController _inventoryScript;
    private ISelector _selector;

    private void Start()
    {
        _selector = _selectionController.GetComponent<ISelector>();
        _inventoryScript = _inventoryController.GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var selection = _selector.GetSelection();
            if (selection != null)
            {
                if (selection.gameObject.layer == LayerMask.NameToLayer("Collectable"))
                {
                    var droppedItem = selection.gameObject.GetComponent<DroppedItem>();
                    if (droppedItem != null)
                    {
                        _inventoryScript.AddItem(droppedItem.Item, droppedItem.Amount);
                        Destroy(selection.gameObject);
                    }
                }
            }
        }
    }
}
