using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the animations of a character, blocking certain animations
/// based on character state.
/// </summary>
public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
{
    Animator animator;
    IWeaponAnimator weaponAnimator;

    void Start() {
        animator = GetComponent<Animator>();
        weaponAnimator = GetComponentInChildren<IWeaponAnimator>();
    }

    /// <summary>
    /// Tries to make the character attack.
    /// </summary>
    /// <returns></returns>
    public bool Attack() {
        weaponAnimator.Attack();
        return true;
    }
}
