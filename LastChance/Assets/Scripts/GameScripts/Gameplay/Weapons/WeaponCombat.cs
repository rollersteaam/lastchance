using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls what a weapon does in combat.
/// </summary>
public class WeaponCombat : MonoBehaviour
{
    [SerializeField]
    int weaponDamage = 10;
    GameObject wielder;
    AttackTrigger attackTrigger;

    void Start()
    {
        wielder = transform.parent.gameObject;
        attackTrigger = GetComponentInChildren<AttackTrigger>();

        attackTrigger.OnAttackHit += OnAttackHit;
    }

    void OnAttackHit(object sender, AttackHitEventArgs attackHit) {
        attackHit.Damageable.Damage(wielder, weaponDamage);
    }
}
