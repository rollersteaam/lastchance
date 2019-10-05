using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Binds control input to player character modules.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    Character character;
    IMoveable moveable;
    CharacterCombat combat;

    void Start()
    {
        character = GetComponent<Character>();
        moveable = GetComponent<IMoveable>();
        combat = GetComponent<CharacterCombat>();
    }

    void FixedUpdate()
    {
        if (!character.healthProperties.alive) return;

        ProcessMovementInput();
        ProcessRotationInput();
        ProcessAttackInput();
    }

    /// <summary>
    /// Rotates the object based on mouse pointer location.
    /// </summary>
    void ProcessRotationInput()
    {
        moveable.TurnToScreenPoint(Input.mousePosition);
    }

    /// <summary>
    /// Makes object attack based on Fire1 input axis.
    /// </summary>
    void ProcessAttackInput()
    {
        if (Input.GetAxis("Fire1") == 0) return;

        combat.Attack();
    }

    /// <summary>
    /// Moves object based on Horizontal and Vertical input axes.
    /// </summary>
    void ProcessMovementInput() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0) return;

        moveable.Move(new Vector2(h, v));
    }
}
