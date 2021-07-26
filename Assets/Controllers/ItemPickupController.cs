using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupController : MonoBehaviour
{
    public static ItemPickupController Instance;
    
    private InteractionTargetSelector _interactionTargetSelector;
    private InventoryController _inventoryController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _interactionTargetSelector = InteractionTargetSelector.Instance;
        _inventoryController = InventoryController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Transform interactionTarget = _interactionTargetSelector.GetInteractionTarget();
            if (interactionTarget != null)
            {
                if (interactionTarget.gameObject.layer == LayerMask.NameToLayer("Collectable"))
                {
                    ItemInstance itemInstance = interactionTarget.gameObject.GetComponent<ItemInstance>();
                    if (itemInstance != null)
                    {
                        _inventoryController.AddItem(itemInstance.Item, itemInstance.Amount);
                        Destroy(interactionTarget.gameObject);
                    }
                }
            }
        }
    }
}
