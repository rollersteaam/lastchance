﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classes that cause movement.
/// </summary>
public interface IMoveable
{
    /// <summary>
    /// <para>
    /// Moves the object in the direction.
    /// </para>
    /// <para>
    /// Input vector is normalized, so magnitude is ignored.
    /// </para>
    /// </summary>
    /// <param name="direction">
    /// Normalized direction vector. Magnitude is ignored.
    /// </param>
    void Move(Vector2 direction);
}