using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageSource
{
    AudioClip GetHitSound();
}

/// <summary>
/// Moves object with a constant force in one direction.
/// </summary>
public class Bullet : MonoBehaviour, IDamageSource
{
    public AudioClip anticipationSound;
    Rigidbody2D rb;

    [SerializeField] AudioClip hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * 18, ForceMode2D.Impulse);
    }

    public AudioClip GetHitSound()
    {
        return hitSound;
    }
}
