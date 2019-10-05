using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modules that judge whether an enemy should attack.
/// </summary>
public interface IEnemyAttacker
{
    bool ShouldAttack(float distance);
}
