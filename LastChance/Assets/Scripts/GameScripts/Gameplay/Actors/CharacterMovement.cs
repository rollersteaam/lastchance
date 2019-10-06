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
        rb.AddForce(
            direction.normalized
            * character.movementProperties.speed
        );
    }

    public void TurnToScreenPoint(Vector3 target)
    {
        Vector3 diff = target - Camera.main.WorldToScreenPoint(transform.position);
        diff.Normalize();

        float zRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(
            0f,
            0f,
            zRot - 90f
        );
    }

    public void Knockback(Vector3 source, float amount)
    {
        Vector3 diff = transform.position - source;
        diff.Normalize();

        rb.AddForce(diff * amount);
    }
}
