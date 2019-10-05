using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes melee weapons attack. Depends on the 'Attack'
/// animation state existing.
/// </summary>
public class MeleeAttacker : MonoBehaviour, IWeaponAttacker
{
    [SerializeField]
    int weaponDamage = 10;
    Animator animator;
    GameObject wielder;
    AttackTrigger attackTrigger;

    void Start()
    {
        animator = GetComponent<Animator>();
        wielder = transform.parent.gameObject;

        // Initialise attack trigger
        attackTrigger = GetComponentInChildren<AttackTrigger>();
        if (attackTrigger != null)
        {
            attackTrigger.OnAttackHit += OnAttackHit;
        }
    }

    void OnAttackHit(object sender, AttackHitEventArgs attackHit)
    {
        attackHit.Damageable.Damage(wielder, weaponDamage);
    }

    public void Attack()
    {
        animator.Play("Attack", -1, 0f);
    }
}
