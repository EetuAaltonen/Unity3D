using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    public Image InventoryScreen;
    public bool InventoryOpen;
    public Camera ItemInspectorCamera;

    [SerializeField] private GameObject _itemInspectorLocatorRef;
    [SerializeField] private GameObject _itemInspectorHolderRef;
    private InspectorHolderZoom _holderZoomScript;
    private RotateViewer _rotateScript;

    // Start is called before the first frame update
    void Start()
    {
        InventoryOpen = false;
        InventoryScreen.gameObject.SetActive(InventoryOpen);
        ItemInspectorCamera.gameObject.SetActive(InventoryOpen);

        _holderZoomScript = _itemInspectorLocatorRef.GetComponent<InspectorHolderZoom>();
        _rotateScript = _itemInspectorHolderRef.GetComponent<RotateViewer>();
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
                _holderZoomScript.ResetPosition();
                _rotateScript.SwapItem(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
