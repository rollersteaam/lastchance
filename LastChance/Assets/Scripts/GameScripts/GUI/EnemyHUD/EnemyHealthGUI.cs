using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides serices and events to manage the UI of enemy health.
/// </summary>
public class EnemyHealthGUI : MonoBehaviour
{
    Character character;
    HealthMaskGUI healthMask;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        character = transform.parent.GetComponent<Character>();
        healthMask = GetComponentInChildren<HealthMaskGUI>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var health = character.healthProperties.health;
        var maxHealth = character
            .evolutionProperties
            .CurrentEvolution
            .CalculateMaximumHealth() * Difficulty.Instance.GetHealthMult(character.gameObject);
            
        healthMask.SetHealth(health, maxHealth);
    }
}
