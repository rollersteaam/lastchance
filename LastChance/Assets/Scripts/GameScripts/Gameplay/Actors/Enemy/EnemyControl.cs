using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// Controls AI character decisions based on current world context.
/// </para>
/// <para>
/// Depends on an <see cref="IMoveable"/> script.
/// </para>
/// </summary>
public class EnemyControl : MonoBehaviour
{
    Transform player;
    IMoveable moveable;
    IEnemyAttackJudge attackJudge;
    IAttacker attacker;

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        moveable = GetComponent<IMoveable>();
        attackJudge = GetComponent<IEnemyAttackJudge>();
        attacker = GetComponent<IAttacker>();
    }

    void FixedUpdate() {
        EvaluatePosition();
    }

    /// <summary>
    /// Evaluates the current position of the enemy to decide whether to
    /// attack or not.
    /// </summary>
    void EvaluatePosition() {
        Vector2 difference = player.position - transform.position;

        if (attackJudge.ShouldAttack(difference.magnitude)) {
            attacker.Attack();
        } else {
            moveable.Move(difference);
        }
    }
}
