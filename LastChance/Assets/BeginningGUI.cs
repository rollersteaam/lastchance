using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningGUI : MonoBehaviour
{
    CanvasGroup group;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void Begin()
    {
        ProgressionManager.Instance.Begin();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
