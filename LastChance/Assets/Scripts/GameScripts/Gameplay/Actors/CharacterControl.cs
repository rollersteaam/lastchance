using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Binds input to an object that implements <see cref="IMoveable"/>.
/// </summary>
public class CharacterControl : MonoBehaviour
{
    IMoveable moveable;

    void Start()
    {
        moveable = GetComponent<IMoveable>();
    }

    void FixedUpdate()
    {
        ProcessMovementInput();
    }

    /// <summary>
    /// Moves object based on horizontal and vertical axes.
    /// </summary>
    void ProcessMovementInput() {
        moveable.Move(
            new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            )
        );
    }
}
