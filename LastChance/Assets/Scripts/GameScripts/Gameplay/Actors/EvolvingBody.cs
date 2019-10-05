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
        character.evolutionProperties.CurrentEvolution = evolution;
    }
}
