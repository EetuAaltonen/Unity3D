using UnityEngine;

public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
{
    public Camera firstPersonCamera, thirdPersonCamera;

    public Ray CreateRay()
    {
        return firstPersonCamera.gameObject.activeSelf ? firstPersonCamera.ScreenPointToRay(Input.mousePosition) : thirdPersonCamera.ScreenPointToRay(Input.mousePosition);
    }
}