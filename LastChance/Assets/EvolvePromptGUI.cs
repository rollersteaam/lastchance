using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvePromptGUI : MonoBehaviour
{
    CanvasGroup group;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        group = GetComponent<CanvasGroup>();

        SpawnManager.Instance.OnBossKill += (o, e) => {
            group.alpha = 1;

            Chrono.Instance.After(9f, () => {
                group.alpha = 0;
            });
        };
    }
}
