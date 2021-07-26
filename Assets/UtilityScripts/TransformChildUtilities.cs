using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformChildUtilities
{
    public static Transform FindChildTransformRecursive(Transform transform, string childName)
    {
        Transform childTransform = null;
        foreach (Transform child in transform)
        {
            if (child.name == childName)
            {
                childTransform = child;
                break;
            }
            else
            {
                childTransform = FindChildTransformRecursive(child, childName);
                if (childTransform != null) break;
            }
        }
        return childTransform;
    }
}
