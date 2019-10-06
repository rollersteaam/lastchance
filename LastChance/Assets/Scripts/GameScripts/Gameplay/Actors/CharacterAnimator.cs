using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the animations of a character, blocking certain animations
/// based on character state.
/// </summary>
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    IWeaponAttacker weaponAttacker;
    SpriteRenderer sr;
    bool hitflashing;
    Shader hitFlashShader;
    Shader defaultShader;
    Color defaultColor;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponAttacker = GetComponentInChildren<IWeaponAttacker>();
        sr = GetComponent<SpriteRenderer>();

        hitFlashShader = Shader.Find("GUI/Text Shader");
        defaultShader = sr.material.shader;
        defaultColor = sr.color;
    }

    /// <summary>
    /// Tries to make the character attack.
    /// </summary>
    /// <returns></returns>
    public bool Attack()
    {
        if (weaponAttacker == null)
        {
            weaponAttacker = GetComponentInChildren<IWeaponAttacker>();
        }

        weaponAttacker.Attack();
        return true;
    }

    /// <summary>
    /// Causes the sprite to flash momentarily, to replicate an arcade hit
    /// effect.
    /// </summary>
    public void HitFlash()
    {
        if (hitflashing) return;

        hitflashing = true;

        sr.material.shader = hitFlashShader;
        sr.color = Color.white;

        Chrono.Instance.After(0.2f, () =>
        {
            if (sr != null)
            {
                sr.material.shader = defaultShader;
                sr.color = defaultColor;
            }

            hitflashing = false;
        });
    }

    public void CancelAnimation()
    {
        if (weaponAttacker == null)
        {
            weaponAttacker = GetComponentInChildren<IWeaponAttacker>();
        }

        weaponAttacker.Cancel();
    }
}
