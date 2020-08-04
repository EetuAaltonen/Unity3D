using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HighlightSelectionResponse : MonoBehaviour
{
    public Material highlightMaterial;
    public Material tempMaterial;

    public void OnSelect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            tempMaterial = selectionRenderer.material;
            selectionRenderer.material = highlightMaterial;
        }
    }

    public void OnDeselect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = tempMaterial;
            //tempSelection = null;
        }
    }
}