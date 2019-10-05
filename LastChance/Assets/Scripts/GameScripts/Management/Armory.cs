using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmoryWeapons {
    public GameObject sword;

    /// <summary>
    /// TODO: IMPLEMENT
    /// Randomly chooses a weapon.
    /// </summary>
    /// <returns></returns>
    public GameObject Choose() {
        // TODO: i've noticed this is a pattern.
        //  We could implement some kind of InspectorCollection or something?
        return sword;
    }
}

/// <summary>
/// Stores and offers a plethora of weapons for one to choose from.
/// </summary>
public class Armory : MonoBehaviour
{
    public ArmoryWeapons weapons;
}
