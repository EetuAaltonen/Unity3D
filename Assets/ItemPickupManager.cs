using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemPickupManager : MonoBehaviour
{
    public SelectionManager Manager;
    public InventoryController InventoryController;
    private ISelector _selector;

    private void Start()
    {
        _selector = Manager.GetComponent<ISelector>();
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
                        InventoryController.AddItem(droppedItem.Item, droppedItem.Amount);
                        Destroy(selection.gameObject);
                    }
                }
            }
        }
    }
}
