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

    void Start()
    {
        character = GetComponent<Character>();
        characterAnimator = GetComponent<CharacterAnimator>();

        armory = GameObject.FindWithTag("Armory").GetComponent<Armory>();

        GetWeaponFromArmory();
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
    public void Damage(GameObject attacker, int amount)
    {
        // No hurting yourself!!!
        if (attacker == gameObject) return;
        // Hitting the deceased is not very nice at all.
        if (!character.healthProperties.alive) return;

        // Apply damage multipliers
        var atkChar = attacker.GetComponent<Character>();
        if (atkChar != null)
        {
            var dmgMult = atkChar
                .evolutionProperties
                .CurrentEvolution
                .damageMultiplier;
            amount = Mathf.RoundToInt(
                amount *
                dmgMult *
                Difficulty.Instance.GetDamageMult(attacker)
            );
        }

        character.healthProperties.health -= amount;

        if (character.healthProperties.health <= 0)
        {
            character.healthProperties.health = 0;
            Kill();
        }
    }

    /// <summary>
    /// Gets weapon from armory service if the actor wasn't already provided
    /// one.
    /// </summary>
    void GetWeaponFromArmory()
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
