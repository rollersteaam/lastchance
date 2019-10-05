using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls what happens to a character in combat situations.
/// </summary>
public class CharacterCombat : MonoBehaviour, IDamageable
{
    Character character;

    void Start() {
        character = GetComponent<Character>();
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
                .currentEvolution
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
    }
}
