using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractionLayer
{
    public LayerMask Layer;
    public float MaxDistance;
    public InteractionLayer(LayerMask layer, float maxDistance)
    {
        Layer = layer;
        MaxDistance = maxDistance;
    }
}

