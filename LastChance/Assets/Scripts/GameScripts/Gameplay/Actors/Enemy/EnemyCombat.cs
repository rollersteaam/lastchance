using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Methods to judge and make an enemy fight.
/// </summary>
public class EnemyCombat : MonoBehaviour, IEnemyAttackJudge, IAttacker
{
    /// <summary>
    /// How far an enemy can attack.
    /// </summary>
    [SerializeField]
    float attackDistance;

    /// <summary>
    /// Makes the character attack.
    /// </summary>
    public void Attack() {

    }

    /// <summary>
    /// Whether the enemy should attack.
    /// </summary>
    /// <param name="distance">Distance from an enemy.</param>
    /// <returns></returns>
    public bool ShouldAttack(float distance) {
        return distance <= attackDistance;
    }
}
