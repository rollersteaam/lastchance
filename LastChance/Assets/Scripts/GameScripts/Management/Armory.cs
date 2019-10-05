using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmoryWeapons {
    // TODO: Implement this process as a pattern
    public GameObject sword;
    public GameObject gun;

    /// <summary>
    /// Randomly chooses a weapon from the armory.
    /// </summary>
    /// <returns></returns>
    public GameObject Choose() {
        switch (UnityEngine.Random.Range(0, 2)) {
            case 0:
                return sword;
            case 1:
                return gun;
            default:
                throw new System.NotImplementedException();
        }
    }
}

/// <summary>
/// Stores and offers a plethora of weapons for one to choose from.
/// </summary>
public class Armory : MonoBehaviour
{
    public ArmoryWeapons weapons;
}
