using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDataUtilities
{
    public static bool CompareItems(ItemData item1, ItemData item2)
    {
        if (item1.Id != item2.Id) return false;
        if (item1.Name != item2.Name) return false;
        if (item1.Type != item2.Type) return false;

        if (item1.StatModifier.Count != item2.StatModifier.Count) return false;
        for (int i = 0; i < item1.StatModifier.Count; i++)
        {
            if (!CompareItemStatModifier(item1.StatModifier[i], item2.StatModifier[i])) return false;
        }
        return true;
    }

    public static bool CompareItemStatModifier(StatModifier statModifier1, StatModifier statModifier2)
    {
        if (statModifier1.Type != statModifier2.Type) return false;
        if (statModifier1.Amount != statModifier2.Amount) return false;
        if (statModifier1.Durability != statModifier2.Durability) return false;
        return true;
    }
}
