using System.Collections;
using System.Collections.Generic;
using System.Runtime;

using UnityEngine;

[System.Serializable]
public class MovementProperties {
    /// <summary>
    /// How fast the character moves.
    /// </summary>
    public int speed;
}

/// <summary>
/// Character model. The stats of a character.
/// </summary>
public class Character : MonoBehaviour
{
    public MovementProperties movementProperties;
}
