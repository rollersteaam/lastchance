using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modules that judge whether an enemy should attack.
/// </summary>
public interface IEnemyAttackJudge
{
    bool ShouldAttack(float distance);
}
