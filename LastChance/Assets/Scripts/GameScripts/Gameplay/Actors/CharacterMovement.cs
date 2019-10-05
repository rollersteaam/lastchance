using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Methods for characters to move. Uses rigidbody.
/// </summary>
public class CharacterMovement : MonoBehaviour, IMoveable
{
    Character character;
    Rigidbody2D rb;

    void Start()
    {
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        rb.AddRelativeForce(
            direction.normalized * character.movementProperties.speed
        );
    }

    public void TurnTo(Vector2 target)
    {
        transform.rotation.SetLookRotation(target);
        // TODO: Alternatively set Euler rotation using trigonometry to vector target
    }
}
