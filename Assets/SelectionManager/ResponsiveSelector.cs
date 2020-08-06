﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class ResponsiveSelector : MonoBehaviour, ISelector
{
    public LayerMask SelectableLayer;
    public float Treshold;
    public Transform PlayerLocation;
    public float MaxDistance;
    private Transform _selection;
    
    public void Check(Ray ray)
    {
        _selection = null;

        GameObject[] gameObjectArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var vector1 = ray.direction;
        var closest = 0f;
        foreach (var gameObject in gameObjectArray)
        {
            if (Vector3.Distance(gameObject.transform.position, PlayerLocation.position) <= MaxDistance)
                if (SelectableLayer == (SelectableLayer | (1 << gameObject.layer)))
                {
                    var vector2 = gameObject.transform.position - ray.origin;
                    var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);

                    if (lookPercentage > Treshold && lookPercentage > closest)
                    {
                        closest = lookPercentage;
                        _selection = gameObject.transform;
                    }
                }
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}
