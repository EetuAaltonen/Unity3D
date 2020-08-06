using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public GameObject Prefab;
    public ItemData Data;
}