using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// Evolving bodies transform their sprites and sizes based off evolutions.
/// </para>
/// </summary>
public class EvolvingBody : MonoBehaviour
{
    Character character;
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
        col = GetComponent<CircleCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Evolve(Evolution evolution)
    {
        var camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(
            camPos.x,
            camPos.y,
            evolution.cameraZ    
        );

        col.radius = evolution.colliderRadius;
        rend.sprite = evolution.sprite;
        rb.mass = evolution.mass;
        character.evolutionProperties.currentEvolution = evolution;
    }
}
