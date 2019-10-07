using System;
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
    EvolvingBody evolvingBody;
    AudioSource audioSource;

    void Start()
    {
        character = GetComponent<Character>();
        characterAnimator = GetComponent<CharacterAnimator>();
        characterMovement = GetComponent<CharacterMovement>();
        evolvingBody = GetComponent<EvolvingBody>();
        audioSource = GetComponent<AudioSource>();

        armory = ReferenceManager.Instance.armory.GetComponent<Armory>();

        GetWeapon();

        Chrono.Instance.After(0.1f, () => {
            if (gameObject == ReferenceManager.Instance.player)
            {
                evolvingBody.Evolve(character.evolutionProperties.initialEvolution);
            }
        });
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

        audioSource.PlayOneShot(damageSource.GetHitSound(), 4f + 1 * attacker.GetComponent<Character>().evolutionProperties.CurrentEvolution.CalculateStatMul());
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

        ChooseNewWeapon();
    }

    public void ChooseNewWeapon()
    {
        if (character.combatProperties.weapon != null) {
            Destroy(character.combatProperties.weapon);
        }

        character.combatProperties.weapon = Instantiate(
            armory.weapons.Choose(),
            transform.position,
            transform.rotation,
            transform
        );
        character.combatProperties.weapon.transform.localScale = transform.localScale;
    }

    /// <summary>
    /// Kills the current character.
    /// </summary>
    void Kill()
    {
        character.healthProperties.alive = false;
        OnDeath?.Invoke(this, EventArgs.Empty);

        if (gameObject == ReferenceManager.Instance.player)
        {
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
