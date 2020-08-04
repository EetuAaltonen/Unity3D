using UnityEngine;

internal interface ISelector
{
    Transform GetSelection();
    void Check(Ray ray);
}
