using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves object with a constant force in one direction.
/// </summary>
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * 3, ForceMode2D.Impulse);
    }
}
