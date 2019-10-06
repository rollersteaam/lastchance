using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classes that make weapons attack.
/// </summary>
public interface IWeaponAttacker
{
    bool Attack();
    /// <summary>
    /// Whether the weapon is in range or not.
    /// </summary>
    /// <returns></returns>
    bool InRange(float targetDistance);
    void Cancel();
}
