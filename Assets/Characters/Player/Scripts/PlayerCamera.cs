using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float SensitivityX = 100f;
    public float SensitivityY = 100f;

    public Transform PlayerBody;
    public Transform FirstPersonCam;
    public Transform ThirdPersonCam;

    public float Speed = 6f;

    float TurnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    float xRotation = 0f;

    GameObject playerRef;
    ChangePOV povScript;
    GameObject uiControllerRef;
    ToggleInventory inventoryScript;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        playerRef = GameObject.Find("Player");
        povScript = playerRef.GetComponent<ChangePOV>();
        uiControllerRef = GameObject.Find("UIController");
        inventoryScript = uiControllerRef.GetComponent<ToggleInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryScript.InventoryOpen)
        {
            if (povScript.IsFirstPerson) {
                float mouseX = Input.GetAxis("Mouse X") * SensitivityX * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * SensitivityY * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                FirstPersonCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                PlayerBody.Rotate(Vector3.up * mouseX);
            } else {
                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(x, 0f, z).normalized;

                if (direction.magnitude >= 0.1f) {
                    float targetAngle = ThirdPersonCam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TurnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    /*float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);*/
                }
            }
        }
    }
}
