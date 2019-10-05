using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// Manages the evolving body of a character.
/// </para>
/// <para>
/// Evolving bodies transform their sprites and sizes based off evolutions.
/// </para>
/// </summary>
public class EvolvingBody : MonoBehaviour
{
    Character character;
    EvolutionLibrary library;
    CircleCollider2D col;
    SpriteRenderer rend;
    Rigidbody2D rb;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        character = GetComponent<Character>();
        library = GameObject.FindWithTag("EvolutionLibrary")
            .GetComponent<EvolutionLibrary>();
        col = GetComponent<CircleCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Initialise the character as their evolution.
        Evolve(character.evolutionProperties.initialEvolution);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Character otherChar = other.gameObject.GetComponent<Character>();

        if (otherChar == null) return;

        AttemptEvolve(otherChar);
    }

    public void Evolve(EvolutionType evolutionType)
    {
        Evolution evolution = library.GetEvolution(evolutionType);

        var camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(
            camPos.x,
            camPos.y,
            evolution.cameraZ
        );
        col.radius = evolution.colliderRadius;
        rend.sprite = evolution.sprite;
        rb.mass = evolution.mass;

        character.healthProperties.health = evolution.CalculateMaximumHealth();
        character.movementProperties.speed = evolution.CalculateMaximumSpeed();

        character.evolutionProperties.CurrentEvolution = evolution;
    }

    /// <summary>
    /// Attempts to consume and evolve from another character object.
    /// </summary>
    /// <param name="character"></param>
    void AttemptEvolve(Character target)
    {
        bool targetStillAlive = target.healthProperties.alive;
        if (targetStillAlive) return;

        bool currentlyDead = !character.healthProperties.alive;
        if (currentlyDead) return;

        EvolutionType targetEvolution = target
            .evolutionProperties
            .CurrentEvolution
            .type;
        EvolutionType currentEvolution = character
            .evolutionProperties
            .CurrentEvolution
            .type;
        bool invalidEvolution = targetEvolution != currentEvolution;
        Debug.Log("You're not suitable to evolve from this.");
        if (invalidEvolution) return;

        Destroy(target.gameObject);
        Evolve(character.evolutionProperties.CurrentEvolution.nextEvolution);
    }
}
