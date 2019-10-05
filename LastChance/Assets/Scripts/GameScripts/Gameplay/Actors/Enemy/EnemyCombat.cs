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
    float attackDistance = 3;
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
    public void Attack() {
        if (!canAttack) return;

        animator.Attack();
        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    /// <summary>
    /// Whether the enemy should attack.
    /// </summary>
    /// <param name="distance">Distance from an enemy.</param>
    /// <returns></returns>
    public bool ShouldAttack(float distance) {
        return distance <= attackDistance;
    }

    private IEnumerator AttackDelay()
    {
        float duration = 0.5f;

        float normalizedTime = 0;
        while(normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        canAttack = true;
    }
}
