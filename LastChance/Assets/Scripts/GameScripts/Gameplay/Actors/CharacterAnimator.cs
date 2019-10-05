using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the animations of a character, blocking certain animations
/// based on character state.
/// </summary>
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    IWeaponAttacker weaponAttacker;

    void Start() {
        animator = GetComponent<Animator>();
        weaponAttacker = GetComponentInChildren<IWeaponAttacker>();
    }

    /// <summary>
    /// Tries to make the character attack.
    /// </summary>
    /// <returns></returns>
    public bool Attack() {
        weaponAttacker.Attack();
        return true;
    }
}
