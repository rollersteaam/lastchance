using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires projectiles and listens to their hit event.
/// </summary>
public class RangedAttacker : MonoBehaviour, IWeaponAttacker
{
    [SerializeField] GameObject projectile;
    [SerializeField] int attackDamage;
    [SerializeField] float attackDelay;
    [SerializeField] float attackPermanence;
    [SerializeField] float attackDistanceRange;
    bool canAttack = true;
    List<GameObject> projectiles = new List<GameObject>();
    Transform dynamicObjects;
    Animator animator;

    void Start()
    {
        dynamicObjects = GameObject.FindWithTag("DynamicObjects").transform;
        animator = GetComponent<Animator>();
    }

    public bool Attack()
    {
        if (!canAttack)
            return false;

        animator.Play("Anticipation");

        // Stop attack so anticipation can fire event for FireProjectile()
        canAttack = false;

        return true;
    }

    public void FireProjectile()
    {
        var proj = Instantiate(
            projectile,
            transform.position,
            transform.rotation,
            dynamicObjects
        );

        RegisterProjectileLifetime(proj);
        RegisterAttackTrigger(proj);
        FinishAttack();
    }

    public bool InRange(float targetDistance)
        => targetDistance < attackDistanceRange;

    public void Cancel()
    {
        FinishAttack();        
    }

    void FinishAttack()
    {
        ApplyAttackDelay();
        animator.Play("Stance");
    }

    void RegisterProjectileLifetime(GameObject proj)
    {
        projectiles.Add(proj);
        Chrono.Instance.After(attackPermanence, () =>
        {
            projectiles.Remove(proj);
            Destroy(proj);
        });
    }

    void RegisterAttackTrigger(GameObject proj)
    {
        var at = proj.GetComponent<AttackTrigger>();
        at.OnAttackHit += (o, e) =>
        {
            e.Damageable.Damage(transform.parent.gameObject, attackDamage);
        };
    }

    /// <summary>
    /// Applies attack delay, freeing the ability to attack after the set delay.
    /// </summary>
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
