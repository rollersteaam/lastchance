using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Binds events and services for managing components of the player health UI.
/// </summary>
public class PlayerHealthGUI : MonoBehaviour
{
    Character player;
    HealthMaskGUI healthMask;

    void Start()
    {
        player = ReferenceManager.Instance.player.GetComponent<Character>();
        healthMask = GetComponentInChildren<HealthMaskGUI>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var health = player.healthProperties.health;
        var maxHealth = player.evolutionProperties.CurrentEvolution.CalculateMaximumHealth();
        healthMask.SetHealth(health, maxHealth);
    }
}
