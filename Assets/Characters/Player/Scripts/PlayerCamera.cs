using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float SensitivityX = 100f;
    public float SensitivityY = 100f;

    public Camera FirstPersonCamera;
    public Camera ThirdPersonCamera;

    public float Speed = 6f;

    [SerializeField] private GameObject _playerRef;
    private float xRotation;

    ChangePOV povScript;
    GameObject uiControllerRef;
    ToggleInventory inventoryScript;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        povScript = GetComponent<ChangePOV>();
        uiControllerRef = GameObject.Find("UIController");
        inventoryScript = uiControllerRef.GetComponent<ToggleInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryScript.InventoryOpen)
        {
            if (povScript.IsFirstPerson()) {
                float mouseX = Input.GetAxisRaw("Mouse X") * SensitivityX * Time.deltaTime;
                float mouseY = Input.GetAxisRaw("Mouse Y") * SensitivityY * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                FirstPersonCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                _playerRef.transform.Rotate(Vector3.up * mouseX);
            }
        }
    }

    public void ResetXRotation()
    {
        xRotation = 0f;
    }
}
