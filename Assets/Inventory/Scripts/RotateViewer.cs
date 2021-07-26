﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateViewer : MonoBehaviour
{
    public ItemData Item;
    public bool SwapItemRequest;
    public float RotateSpeed = 400;

    [SerializeField] private GameObject _itemHolderLocatorRef;
    private InspectorHolderZoom _holderScript;

    // Start is called before the first frame update
    void Start()
    {
        _holderScript = _itemHolderLocatorRef.GetComponent<InspectorHolderZoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwapItemRequest)
        {
            SwapItemRequest = false;
            if (Item != null)
            {
                SwapItem(Item.InstancePrefab);
                _holderScript.ResetPosition();
            }
            else
            {
                SwapItem(null);
            }
        }

        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime;

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

        if (prefab != null)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 65f);

            GameObject viewItemInstance = Instantiate(
                Item.InstancePrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity
            ) as GameObject;
            viewItemInstance.transform.parent = transform;
            viewItemInstance.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            SetLayerRecursively(viewItemInstance, LayerMask.NameToLayer("ItemInspector"));
            InstancePrefabUtilities.DisableItemInstancePhysics(viewItemInstance);
        }
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
