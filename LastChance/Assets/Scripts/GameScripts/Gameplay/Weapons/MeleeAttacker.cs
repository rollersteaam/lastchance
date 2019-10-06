using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes melee weapons attack. Depends on the 'Attack'
/// animation state existing.
/// </summary>
public class MeleeAttacker : MonoBehaviour, IWeaponAttacker
{
    [SerializeField] int weaponDamage = 10;
    [SerializeField] float weaponRange = 3;
    [SerializeField] float attackDelay;
    bool canAttack = true;
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

    public bool Attack()
    {
        if (!canAttack)
            return false;

        animator.Play("Attack", -1, 0f);

        ApplyAttackDelay();

        return true;
    }

    public bool InRange(float targetDistance)
        => targetDistance < weaponRange;

    public void Cancel()
    {
        animator.Play("Stance");
    }

    void ApplyAttackDelay()
    {
        canAttack = false;
        var delayMult = Difficulty.Instance.GetAttackDelayMult(
            transform.parent.gameObject
        );
        Chrono.Instance.After(attackDelay * delayMult, () =>
        {
            canAttack = true;
        });
    }
}
