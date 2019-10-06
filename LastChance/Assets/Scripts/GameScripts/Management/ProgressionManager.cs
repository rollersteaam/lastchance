using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the progression of the game.
/// </summary>
public class ProgressionManager : Singleton<ProgressionManager>
{
    public void TryAgain()
    {
        SceneManager.LoadScene("Genesis");
    }
}
