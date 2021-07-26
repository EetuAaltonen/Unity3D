using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    public static HighlightController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHighlight(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 5;
        }
    }

    public void ClearHighlight(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 0;
        }
    }
}
