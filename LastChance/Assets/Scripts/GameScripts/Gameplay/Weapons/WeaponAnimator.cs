using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard animation controller for weaponry.
/// </summary>
public class WeaponAnimator : MonoBehaviour, IWeaponAnimator
{
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.Play("Attack", -1, 0f);
    }
}
