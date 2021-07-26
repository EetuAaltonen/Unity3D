using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColliderDraw : MonoBehaviour
{
    private Color _color = Color.red;

    private Vector3 _frontTopLeft;
    private Vector3 _frontTopRight;
    private Vector3 _frontBottomLeft;
    private Vector3 _frontBottomRight;
    private Vector3 _backTopLeft;
    private Vector3 _backTopRight;
    private Vector3 _backBottomLeft;
    private Vector3 _backBottomRight;

    private void Update()
    {
        DrawBox();
    }

    void DrawBox()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;

        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        _frontTopLeft = new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z);  // Front top left corner
        _frontTopRight = new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z);  // Front top right corner
        _frontBottomLeft = new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z);  // Front bottom left corner
        _frontBottomRight = new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z);  // Front bottom right corner
        _backTopLeft = new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z);  // Back top left corner
        _backTopRight = new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z);  // Back top right corner
        _backBottomLeft = new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z);  // Back bottom left corner
        _backBottomRight = new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z);  // Back bottom right corner

        _frontTopLeft = transform.TransformPoint(_frontTopLeft);
        _frontTopRight = transform.TransformPoint(_frontTopRight);
        _frontBottomLeft = transform.TransformPoint(_frontBottomLeft);
        _frontBottomRight = transform.TransformPoint(_frontBottomRight);
        _backTopLeft = transform.TransformPoint(_backTopLeft);
        _backTopRight = transform.TransformPoint(_backTopRight);
        _backBottomLeft = transform.TransformPoint(_backBottomLeft);
        _backBottomRight = transform.TransformPoint(_backBottomRight);

        Debug.DrawLine(_frontTopLeft, _frontTopRight, _color);
        Debug.DrawLine(_frontTopRight, _frontBottomRight, _color);
        Debug.DrawLine(_frontBottomRight, _frontBottomLeft, _color);
        Debug.DrawLine(_frontBottomLeft, _frontTopLeft, _color);

        Debug.DrawLine(_backTopLeft, _backTopRight, _color);
        Debug.DrawLine(_backTopRight, _backBottomRight, _color);
        Debug.DrawLine(_backBottomRight, _backBottomLeft, _color);
        Debug.DrawLine(_backBottomLeft, _backTopLeft, _color);

        Debug.DrawLine(_frontTopLeft, _backTopLeft, _color);
        Debug.DrawLine(_frontTopRight, _backTopRight, _color);
        Debug.DrawLine(_frontBottomRight, _backBottomRight, _color);
        Debug.DrawLine(_frontBottomLeft, _backBottomLeft, _color);
    }
}
