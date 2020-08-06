using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorHolderZoom : MonoBehaviour
{
    public int ZoomSteps;
    public float MinDistance;
    public float MaxDistance;
    public Camera Camera;
    public float CameraCenterXOffset;
    public float CameraCenterYOffset;

    private Vector3 _defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = transform.position;
        var zoomCenterPosition = new Vector3(
            Camera.transform.position.x + CameraCenterXOffset,
            Camera.transform.position.y + CameraCenterYOffset,
            Camera.transform.position.z
        );
        transform.rotation = Quaternion.LookRotation((zoomCenterPosition - transform.position).normalized);
    }

    // Update is called once per frame
    void Update()
    {
        var mouseAxis = Input.GetAxis("Mouse ScrollWheel");
        if (mouseAxis != 0f)
        {
            var direction = mouseAxis > 0 ? 1 : -1;
            float dist = Vector3.Distance(Camera.transform.position, transform.position);
            if (direction == 1 && dist > MinDistance || direction == -1 && dist < MaxDistance)
            {
                transform.position += transform.forward * ((ZoomSteps * direction) * Time.deltaTime);
            }
        }
    }

    public void ResetPosition()
    {
        transform.position = _defaultPosition;
    }
}
