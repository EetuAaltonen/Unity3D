using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerUtilities
{
    public static bool IsObjectInLayer(int layerMask, GameObject gameObject)
    {
        return (layerMask == (layerMask | (1 << gameObject.layer)));
    }
    public static bool IsObjectInLayerByName(string layerMaskName, GameObject gameObject)
    {
        var layerMask = LayerMask.NameToLayer(layerMaskName);
        return (layerMask == gameObject.layer);
    }
}
