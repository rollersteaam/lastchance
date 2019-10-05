using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires projectiles and listens to their hit event.
/// </summary>
public class RangedAttacker : MonoBehaviour, IWeaponAttacker
{
    [SerializeField] GameObject projectile;
    [SerializeField] float attackDelay;
    [SerializeField] float attackPermanence;
    bool canAttack;
    List<GameObject> projectiles;
    Transform dynamicObjects;

    void Start()
    {
        dynamicObjects = GameObject.FindWithTag("DynamicObjects").transform;
    }

    public bool Attack() {
        if (!canAttack)
            return false;

        // Manage lifetime of projectile
        var proj = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity,
            dynamicObjects
        );
        projectiles.Add(proj);
        Chrono.Instance.After(attackPermanence, () => {
            projectiles.Remove(proj);
            Destroy(proj);
        });
        
        // Apply attack delay
        canAttack = false;
        Chrono.Instance.After(attackDelay, () => {
            canAttack = true;
        });

        return true;
    }
}
