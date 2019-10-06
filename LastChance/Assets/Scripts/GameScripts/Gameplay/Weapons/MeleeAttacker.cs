using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes melee weapons attack. Depends on the 'Attack'
/// animation state existing.
/// </summary>
public class MeleeAttacker : MonoBehaviour, IWeaponAttacker, IDamageSource
{
    [SerializeField] int weaponDamage = 10;
    [SerializeField] float weaponRange = 3;
    [SerializeField] float attackDelay;
    bool canAttack = true;
    Animator animator;
    GameObject wielder;
    AttackTrigger attackTrigger;
    [SerializeField] AudioClip anticipationSound;
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }

    void OnAttackHit(object sender, AttackHitEventArgs attackHit)
    {
        attackHit.Damageable.Damage(wielder, attackTrigger.GetComponent<IDamageSource>(), weaponDamage);
    }

    public bool Attack()
    {
        if (!canAttack)
            return false;

        canAttack = false;
        animator.Play("Anticipation");
        audioSource.PlayOneShot(anticipationSound);

        return true;
    }

    public void Swing()
    {
        animator.Play("Attack");
    }

    public void FinishAttack()
    {
        ApplyAttackDelay();
        animator.Play("Stance");
    }

    public bool InRange(float targetDistance)
        => targetDistance < weaponRange;

    public void Cancel()
    {
        FinishAttack();
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

    public AudioClip GetHitSound()
    {
        return hitSound;
    }
}
