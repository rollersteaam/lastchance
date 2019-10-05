using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitEventArgs : EventArgs {
    public IDamageable Damageable { get; set; }
}

/// <summary>
/// Basic message-to-event converter to show when an attack has landed.
/// </summary>
public class AttackTrigger : MonoBehaviour
{
    public event EventHandler<AttackHitEventArgs> OnAttackHit;

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("HIT!");

        var damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable == null) return;

        OnAttackHit?.Invoke(this, new AttackHitEventArgs() {
            Damageable = damageable
        });
    }
}
