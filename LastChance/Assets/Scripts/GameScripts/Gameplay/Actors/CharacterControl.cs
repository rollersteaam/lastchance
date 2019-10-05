using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Binds input to character modules.
/// </summary>
public class CharacterControl : MonoBehaviour
{
    IMoveable moveable;
    ICharacterAnimator animator;

    void Start()
    {
        moveable = GetComponent<IMoveable>();
        animator = GetComponent<ICharacterAnimator>();
    }

    void FixedUpdate()
    {
        ProcessMovementInput();
        ProcessAttackInput();
    }

    /// <summary>
    /// Makes object attack based on Fire1 input axis.
    /// </summary>
    void ProcessAttackInput()
    {
        if (Input.GetAxis("Fire1") == 0) return;

        animator.Attack();
    }

    /// <summary>
    /// Moves object based on Horizontal and Vertical input axes.
    /// </summary>
    void ProcessMovementInput() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h == 0 || v == 0) return;

        moveable.Move(new Vector2(h, v));
    }
}
