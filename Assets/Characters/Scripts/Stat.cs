using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float _baseValue;

    private List<StatModifier> modifiers = new List<StatModifier>();

    public float GetValue()
    {
        float modifiedValue = _baseValue;
        modifiers.ForEach(x => modifiedValue += x.Amount);
        return modifiedValue;
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifier.Amount != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(StatModifier modifier)
    {
        if (modifier.Amount != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
