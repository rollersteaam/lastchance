using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains properties that change the difficulty of the game.
/// </summary>
public class Difficulty : Singleton<Difficulty>
{
    public float enemySpeedMultplier = 1;
    public float enemyHealthMultiplier = 1;
    public float enemyAttackDelayMultiplier = 1;
    public float enemyDamageMultiplier = 1;

    public float GetSpeedMult(GameObject go)
        => IsPlayer(go) ? 1 : enemySpeedMultplier;

    public float GetHealthMult(GameObject go)
        => IsPlayer(go) ? 1 : enemyHealthMultiplier;

    public float GetAttackDelayMult(GameObject go)
        => IsPlayer(go) ? 1 : enemyAttackDelayMultiplier;

    public float GetDamageMult(GameObject go)
        => IsPlayer(go) ? 1 : enemyDamageMultiplier;

    bool IsPlayer(GameObject go)
        => go == ReferenceManager.Instance.player;
}
