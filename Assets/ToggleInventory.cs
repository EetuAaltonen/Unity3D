using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    public Image InventoryScreen;
    public bool InventoryOpen;
    public Camera ItemInspectorCamera;

    void Start()
    {
        InventoryOpen = false;
        InventoryScreen.gameObject.SetActive(InventoryOpen);
        ItemInspectorCamera.gameObject.SetActive(InventoryOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryOpen = !InventoryOpen;
            InventoryScreen.gameObject.SetActive(InventoryOpen);
            InventoryScreen.gameObject.GetComponent<DisplayInventory>().RequestInventoryRefresh();
            ItemInspectorCamera.gameObject.SetActive(InventoryOpen);
            if (InventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
