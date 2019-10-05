using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EvolutionType
{
    Force, Quark, Particle, Atom, Compound, Bacteria, Insect, Creature, Animal
}

[System.Serializable]
public struct Evolution
{
    public EvolutionType type;
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

