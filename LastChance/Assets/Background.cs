using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void FixedUpdate()
    {        
        var pos = Camera.main.transform.position;
        transform.position = new Vector3(
            pos.x,
            pos.y,
            transform.position.z
        );
    }

    void Update()
    {
        var amount = Camera.main.transform.position.z / -20;
        transform.localScale = new Vector3(
            amount,
            amount,
            amount
        );
    }
}
