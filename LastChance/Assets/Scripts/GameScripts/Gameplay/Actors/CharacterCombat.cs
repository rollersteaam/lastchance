﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls what happens to a character in combat situations.
/// </summary>
public class CharacterCombat : MonoBehaviour, IDamageable
{
    public static event EventHandler OnPlayerDeath;
    public event EventHandler OnDeath;

    Armory armory;
    Character character;
    CharacterAnimator characterAnimator;
    CharacterMovement characterMovement;
    AudioSource audioSource;

    void Start()
    {
        character = GetComponent<Character>();
        characterAnimator = GetComponent<CharacterAnimator>();
        characterMovement = GetComponent<CharacterMovement>();
        audioSource = GetComponent<AudioSource>();

        armory = GameObject.FindWithTag("Armory").GetComponent<Armory>();

        GetWeapon();
    }

    /// <summary>
    /// A swing and a... miss.
    /// </summary>
    public bool Attack()
    {
        if (!characterAnimator.Attack())
            return false;

        // TODO: Create temporary attack blacklist to avoid double hit

        return true;
    }

    /// <summary>
    /// Damages the current character.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="amount"></param>
    public void Damage(GameObject attacker, IDamageSource damageSource, int amount)
    {
        // No hurting yourself!!!
        if (attacker == gameObject) return;
        // Hitting the deceased is not very nice at all.
        if (!character.healthProperties.alive) return;

        amount = ApplyDamageMultipliers(attacker, amount);

        character.healthProperties.health -= amount;

        if (character.healthProperties.health <= 0)
        {
            character.healthProperties.health = 0;
            Kill();
        }

        characterAnimator.HitFlash();

        if (gameObject.tag != "Player")
        {
            characterMovement.Knockback(
                attacker.transform.position,
                ApplyDamageMultipliers(attacker, 3000)
            );
            characterAnimator.CancelAnimation();
        }

        audioSource.PlayOneShot(damageSource.GetHitSound());
    }

    /// <summary>
    /// Applies damage multipliers to an input damage amount, based on
    /// properties of the attacker's character.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    int ApplyDamageMultipliers(GameObject attacker, int damage)
    {
        var atkChar = attacker.GetComponent<Character>();

        if (atkChar == null) return damage;

        var evoDmgMult = atkChar
            .evolutionProperties
            .CurrentEvolution
            .damageMultiplier;

        damage = Mathf.RoundToInt(
            damage *
            evoDmgMult *
            Difficulty.Instance.GetDamageMult(attacker)
        );

        return damage;
    }

    /// <summary>
    /// Gets weapon from armory service if the actor wasn't already provided
    /// one.
    /// </summary>
    void GetWeapon()
    {
        if (character.combatProperties.weapon != null) return;

        character.combatProperties.weapon = Instantiate(
            armory.weapons.Choose(),
            transform.position,
            Quaternion.identity,
            transform
        );
    }

    /// <summary>
    /// Kills the current character.
    /// </summary>
    void Kill()
    {
        character.healthProperties.alive = false;
        OnDeath?.Invoke(this, EventArgs.Empty);

        if (gameObject.tag == "Player")
        {
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
