using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraAngle : MonoBehaviour
{
    public bool cameraRight;
    public float smoothTime;

    void Start()
    {
        SwitchPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            cameraRight = !cameraRight;
            SwitchPosition();
        }
    }

    private void SwitchPosition()
    {
        var side = cameraRight ? 1 : -1;
        var newPosition = new Vector3(side * 1.5f, 1.5f, 0f);
        transform.localPosition = newPosition;
        //transform.localPosition = Vector3.Lerp(transform.position, newPosition, smoothTime * Time.deltaTime);
    }
}
