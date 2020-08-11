using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float SensitivityX = 100f;
    public float SensitivityY = 100f;
    public Camera FirstPersonCamera;
    public Camera ThirdPersonCamera;

    private float xRotation;
    private ChangePOV _povScript;
    private GameObject _uiControllerRef;
    private ToggleInventory _inventoryScript;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        _povScript = GetComponent<ChangePOV>();
        _uiControllerRef = GameObject.Find("UIController");
        _inventoryScript = _uiControllerRef.GetComponent<ToggleInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inventoryScript.InventoryOpen)
        {
            if (_povScript.IsFirstPerson()) {
                float mouseX = Input.GetAxisRaw("Mouse X") * SensitivityX * Time.deltaTime;
                float mouseY = Input.GetAxisRaw("Mouse Y") * SensitivityY * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                FirstPersonCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.Rotate(transform.up * mouseX);
            }
        }
    }

    public void ResetXRotation()
    {
        xRotation = 0f;
    }
}
