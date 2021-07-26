using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScreenRay : MonoBehaviour
{
    public Camera firstPersonCamera, thirdPersonCamera;

    public Ray CreateRay()
    {
        return firstPersonCamera.gameObject.activeSelf ? firstPersonCamera.ScreenPointToRay(Input.mousePosition) : thirdPersonCamera.ScreenPointToRay(Input.mousePosition);
    }
}
