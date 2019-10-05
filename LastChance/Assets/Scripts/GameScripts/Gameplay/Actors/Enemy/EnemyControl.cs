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
    Character player;
    Character character;
    IMoveable moveable;
    IEnemyAttackJudge attackJudge;
    IAttacker attacker;

    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        character = GetComponent<Character>();
        moveable = GetComponent<IMoveable>();
        attackJudge = GetComponent<IEnemyAttackJudge>();
        attacker = GetComponent<IAttacker>();
    }

    void FixedUpdate() {
        if (!character.healthProperties.alive) return;

        EvaluatePlayerPosition();
    }

    /// <summary>
    /// Evaluates the current position of the enemy to decide whether to
    /// attack or not.
    /// </summary>
    void EvaluatePlayerPosition() {
        if (!player.healthProperties.alive) return;

        Vector2 difference = player.transform.position - transform.position;

        if (attackJudge.ShouldAttack(difference.magnitude)) {
            attacker.Attack();
        } else {
            var playerScreenPos = Camera.main.WorldToScreenPoint(
                player.transform.position
            );
            moveable.TurnToScreenPoint(playerScreenPos);

            moveable.Move(difference);
        }
    }
}
