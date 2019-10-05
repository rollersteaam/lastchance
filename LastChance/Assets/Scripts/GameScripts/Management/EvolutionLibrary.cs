using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EvolutionType
{
    Force, Quark, Particle, Atom, Compound, Bacteria, Insect, Creature, Animal
}

[System.Serializable]
public class Evolution
{
    public EvolutionType type = EvolutionType.Quark;
    /// <summary>
    /// The amount to zoom the camera out by when the player evolves into this
    /// form.
    /// </summary>
    public float cameraZ = -20;
    /// <summary>
    /// The new radius to change character collider size to when evolved into.
    /// </summary>
    public float colliderRadius = 0.936648f;
    /// <summary>
    /// The sprite that represents this form.
    /// </summary>
    public Sprite sprite;
    /// <summary>
    /// The new mass. Mainly affects collisions with smaller enemies.
    /// </summary>
    public float mass;
    /// <summary>
    /// The amount to multiply damage by.
    /// </summary>
    public float damageMultiplier = 1;
}

/// <summary>
/// Contains a map of all the possible evolutions.
/// </summary>
public class EvolutionLibrary : MonoBehaviour
{
    [SerializeField] List<Evolution> possibleEvolutions = new List<Evolution>();
    Dictionary<EvolutionType, Evolution> evolutionMap
        = new Dictionary<EvolutionType, Evolution>();

    void Awake() {
        foreach (var evo in possibleEvolutions) {
            evolutionMap[evo.type] = evo;
        }
    }

    public Evolution GetEvolution(EvolutionType type)
        => evolutionMap[type];
}

