using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the camera follow a target. By default, it follows the player.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    Transform followedTransform;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        followedTransform = ReferenceManager.Instance.player.transform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.position = new Vector3(
            followedTransform.position.x,
            followedTransform.position.y,
            transform.position.z
        );
    }
}
