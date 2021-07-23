using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterUtilities
{
    public static GameObject FindCharacterPlayer()
    {
        return GameObject.Find("Characters/Player");
    }
}
