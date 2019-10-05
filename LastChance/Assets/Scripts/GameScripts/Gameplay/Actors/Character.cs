using System.Collections;
using System.Collections.Generic;
using System.Runtime;

using UnityEngine;

[System.Serializable]
public class MovementProperties {
    /// <summary>
    /// How fast the character moves.
    /// </summary>
    public int speed;
}

[System.Serializable]
public class HealthProperties {
    public int health;
    public bool alive = true;
}

[System.Serializable]
public class EvolutionProperties {
    public EvolutionType initialEvolution;
    public Evolution CurrentEvolution { get; set; }
}

[System.Serializable]
public class CombatProperties {
    public GameObject weapon;
}

/// <summary>
/// Character model. The stats of a character.
/// </summary>
public class Character : MonoBehaviour
{
    public HealthProperties healthProperties;
    public MovementProperties movementProperties;
    public EvolutionProperties evolutionProperties;
    public CombatProperties combatProperties;
}
