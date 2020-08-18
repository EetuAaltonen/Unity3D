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

        if (item1.ItemBuffs.Count != item2.ItemBuffs.Count) return false;
        for (int i = 0; i < item1.ItemBuffs.Count; i++)
        {
            if (!CompareItemBuffs(item1.ItemBuffs[i], item2.ItemBuffs[i])) return false;
        }
        return true;
    }

    public static bool CompareItemBuffs(ItemBuff buff1, ItemBuff buff2)
    {
        if (buff1.Type != buff2.Type) return false;
        if (buff1.Amount != buff2.Amount) return false;
        if (buff1.Durability != buff2.Durability) return false;
        return true;
    }
}
