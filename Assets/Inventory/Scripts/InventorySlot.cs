using UnityEngine;

[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public ItemData Item;
    public bool Selected;
    public GameObject InventoryController;

    public InventorySlot(ItemData item)
    {
        Item = item;
        Selected = false;
    }

    public void ToggleSelected()
    {
        Selected = !Selected;
    }
}
