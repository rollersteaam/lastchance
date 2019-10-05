using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EvolutionType
{
    Force, Quark, Particle, Atom, Compound, Bacteria, Insect, Creature,
    Animal, None
}

[System.Serializable]
public struct Evolution
{
    public EvolutionType type;
    public EvolutionType nextEvolution;
    /// <summary>
    /// The amount to zoom the camera out by when the player evolves into this
    /// form.
    /// </summary>
    public float cameraZ;
    /// <summary>
    /// The new radius to change character collider size to when evolved into.
    /// </summary>
    public float colliderRadius;
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
    public float damageMultiplier;

    /// <summary>
    /// Calculates the multiplier by which all basic stats should be
    /// multiplied by as a result of a linear progression from the base mass
    /// (0.5f).
    /// </summary>
    /// <returns></returns>
    public float CalculateStatMul()
        => mass / 0.5f;

    /// <summary>
    /// Calculates maximum health, deriving it from a linear progression based
    /// off mass (given by <see cref="CalculateStatMul()"/>).
    /// </summary>
    /// <returns></returns>
    public int CalculateMaximumHealth()
        => Mathf.RoundToInt(100 * CalculateStatMul());

    /// <summary>
    /// Calculates max speed based off a base value and
    /// <see cref="CalculateStatMul()"/>.
    /// </summary>
    /// <returns></returns>
    public int CalculateMaximumSpeed()
        => Mathf.RoundToInt(260 * CalculateStatMul());
}

[System.Serializable]
public class Evolutions : IEnumerable<Evolution> {
    public Evolution forceEvolution;
    public Evolution quarkEvolution;
    public Evolution particleEvolution;
    public Evolution atomEvolution;
    public Evolution compoundEvolution;
    public Evolution bacteriaEvolution;
    public Evolution insectEvolution;
    public Evolution creatureEvolution;
    public Evolution animalEvolution;

    public IEnumerator<Evolution> GetEnumerator()
    {
        yield return forceEvolution;
        yield return quarkEvolution;
        yield return particleEvolution;
        yield return atomEvolution;
        yield return compoundEvolution;
        yield return bacteriaEvolution;
        yield return insectEvolution;
        yield return creatureEvolution;
        yield return animalEvolution;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return GetEnumerator();
    }
}

/// <summary>
/// Contains a map of all the possible evolutions.
/// </summary>
public class EvolutionLibrary : MonoBehaviour
{
    [SerializeField] Evolutions evolutions = new Evolutions();
    Dictionary<EvolutionType, Evolution> evolutionMap
        = new Dictionary<EvolutionType, Evolution>();

    void Awake() {
        foreach (var evo in evolutions) {
            evolutionMap[evo.type] = evo;
        }
    }

    public Evolution GetEvolution(EvolutionType type)
        => evolutionMap[type];
}

