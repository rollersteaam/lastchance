using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameOverGUI : MonoBehaviour
{
    GameObject player;
    CanvasGroup canvas;

    void Start()
    {
        player = ReferenceManager.Instance.player;
        canvas = GetComponent<CanvasGroup>();

        CharacterCombat.OnPlayerDeath += OnPlayerDeath;
    }

    void OnPlayerDeath(object sender, System.EventArgs e)
    {
        EnableGameOverScreen();
        AccentuatePlayer();
    }

    public void TryAgain()
    {
        CharacterCombat.OnPlayerDeath -= OnPlayerDeath;

        ProgressionManager.Instance.TryAgain();
    }

    /// <summary>
    /// Changes the render order of the player character for a scenic
    /// death screen.
    /// </summary>
    void AccentuatePlayer()
    {
        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder = 1;
            sr.sortingLayerName = "GUI";
        }
    }

    void EnableGameOverScreen()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}