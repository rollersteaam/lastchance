using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI for actual health portion of a health bar.
/// </summary>
public class HealthMaskGUI : MonoBehaviour
{
    Image image;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// Sets the amount of health displayed as appropriate.
    /// </summary>
    /// <param name="ratio"></param>
    public void SetHealth(float health, float maximumHealth) {
        image.fillAmount = health/maximumHealth;
    }
}
