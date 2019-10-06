using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenGUI : MonoBehaviour
{
    GameObject player;
    CanvasGroup canvas;

    void Start()
    {
        player = ReferenceManager.Instance.player;
        canvas = GetComponent<CanvasGroup>();

        ProgressionManager.OnWin += Win;
    }

    void Win(object sender, System.EventArgs e)
    {
        Debug.Log("SHIT");
        EnableWinScreen();
    }

    public void TryAgain()
    {
        ProgressionManager.OnWin -= Win;
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

    void EnableWinScreen()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}
