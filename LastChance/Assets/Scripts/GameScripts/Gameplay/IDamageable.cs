using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objects that can receive damage.
/// </summary>
public interface IDamageable
{
    void Damage(GameObject attacker, int amount);
}
