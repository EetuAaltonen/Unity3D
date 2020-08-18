using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RotationUtilities
{
    // Start is called before the first frame update
    public static float ScaleRotation(float angle)
    {
        return angle > 180f ? angle - 360f : angle;
    }
}
