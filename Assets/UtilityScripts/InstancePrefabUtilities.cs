using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InstancePrefabUtilities
{
    public static void DisableItemInstancePhysics(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<ItemInstance>().enabled = false;
        gameObject.GetComponent<Outline>().enabled = false;
    }

    public static Vector3 GetEquipPositionOffset(GameObject gameObject)
    {
        Transform equipPosition = gameObject.transform.Find("EquipPosition");
        return equipPosition.localPosition;
    }
}
