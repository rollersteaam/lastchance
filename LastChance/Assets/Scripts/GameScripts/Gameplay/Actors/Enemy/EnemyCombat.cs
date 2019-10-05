using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Methods to judge and make an enemy fight.
/// </summary>
public class EnemyCombat : MonoBehaviour, IAttacker
{
    CharacterAnimator animator;
    bool canAttack = true;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    /// <summary>
    /// Makes the character attack.
    /// </summary>
    public void Attack()
    {
        if (!canAttack) return;

        animator.Attack();

        canAttack = false;
        Chrono.Instance.After(0.5f, () =>
        {
            canAttack = true;
        });
    }
}
