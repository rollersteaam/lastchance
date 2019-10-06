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
    CharacterCombat combat;
    IMoveable moveable;
    IWeaponAttacker weaponAttacker;
    bool weaponAttackerHotloadAttempted;

    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        character = GetComponent<Character>();
        combat = GetComponent<CharacterCombat>();
        moveable = GetComponent<IMoveable>();
        weaponAttacker = GetComponentInChildren<IWeaponAttacker>();
    }

    void FixedUpdate() {
        if (!character.healthProperties.alive) return;

        if (!weaponAttackerHotloadAttempted && weaponAttacker == null) {
            weaponAttacker = GetComponentInChildren<IWeaponAttacker>();
            weaponAttackerHotloadAttempted = true;
        }

        EvaluatePlayerPosition();
    }

    /// <summary>
    /// Evaluates the current position of the enemy to decide whether to
    /// attack or not.
    /// </summary>
    void EvaluatePlayerPosition() {
        if (!player.healthProperties.alive) return;
        if (ProgressionManager.Instance.gameOver) return;

        Vector2 difference = player.transform.position - transform.position;

        var playerScreenPos = Camera.main.WorldToScreenPoint(
            player.transform.position
        );
        moveable.TurnToScreenPoint(playerScreenPos);

        if (weaponAttacker.InRange(difference.magnitude)) {
            combat.Attack();
        } else {
            moveable.Move(difference);
        }
    }
}
