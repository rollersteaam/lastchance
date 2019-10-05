using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls what happens to a character in combat situations.
/// </summary>
public class CharacterCombat : MonoBehaviour, IDamageable
{
    Armory armory;
    Character character;

    public event EventHandler OnDeath;

    void Start() {
        character = GetComponent<Character>();

        armory = GameObject.FindWithTag("Armory").GetComponent<Armory>();
        character.combatProperties.weapon = Instantiate(
            armory.weapons.sword,
            transform.position,
            Quaternion.identity,
            transform
        );
    }

    public void Damage(GameObject attacker, int amount)
    {
        // No hurting yourself!!!
        if (attacker == gameObject) return;
        // Hitting the deceased is not very nice at all.
        if (!character.healthProperties.alive) return;

        // Apply evolution damage multipliers
        var atkChar = attacker.GetComponent<Character>();
        if (atkChar != null) {
            var damageMult = atkChar
                .evolutionProperties
                .CurrentEvolution
                .damageMultiplier;
            amount = Mathf.RoundToInt(amount * damageMult);
        }

        character.healthProperties.health -= amount;

        if (character.healthProperties.health <= 0) {
            character.healthProperties.health = 0;
            Kill();
        }
    }

    /// <summary>
    /// Kills the character.
    /// </summary>
    void Kill() {
        character.healthProperties.alive = false;
        OnDeath?.Invoke(this, EventArgs.Empty);
    }
}
