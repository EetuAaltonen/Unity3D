using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UiElementUtilities
{
    public static void SetSliderValue(GameObject sliderElement, string newValue)
    {
        sliderElement.GetComponent<TMPro.TextMeshProUGUI>().text = newValue;
    }

    public static void SetTextMeshProValue(GameObject textElement, string newValue)
    {
        textElement.GetComponent<TMPro.TextMeshProUGUI>().text = newValue;
    }
}
