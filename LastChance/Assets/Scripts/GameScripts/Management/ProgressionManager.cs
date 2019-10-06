using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the progression of the game.
/// </summary>
public class ProgressionManager : Singleton<ProgressionManager>
{
    public static event System.EventHandler OnWin;
    public bool gameOver;

    public void TryAgain()
    {
        SceneManager.LoadScene("Genesis");
    }

    public void Win()
    {
        Debug.Log("HELLLLLLO?");
        gameOver = true;
        OnWin?.Invoke(this, System.EventArgs.Empty);
    }
}
