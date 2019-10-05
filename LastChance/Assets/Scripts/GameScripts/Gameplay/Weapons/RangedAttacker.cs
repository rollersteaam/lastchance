using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires projectiles and listens to their hit event.
/// </summary>
public class RangedAttacker : MonoBehaviour, IWeaponAttacker
{
    [SerializeField] GameObject projectile;
    List<GameObject> projectiles;

    public void Attack() {
        
    }
}
