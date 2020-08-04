using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateViewer : MonoBehaviour
{
    public ItemObject item;
    public bool swapItem;
    float rotateSpeed = 400;

    void Update()
    {
        if (swapItem)
        {
            swapItem = false;
            if (item)
            {
                SwapItem(item.Prefab);
            }
        }

        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, -rotX, Space.World);
            transform.Rotate(Vector3.right, rotY, Space.World);
        }
    }

    public void SwapItem(GameObject prefab)
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        GameObject gameObject = Instantiate(
            item.Prefab,
            new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity
        ) as GameObject;
        gameObject.transform.parent = transform;
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        SetLayerRecursively(gameObject, LayerMask.NameToLayer("ItemInspector"));
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
