using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quick access for doing things after a certain time.
/// </summary>
public class Chrono : Singleton<Chrono>
{
    IEnumerator Timer(float seconds, System.Action action)
    {
        float progressed = 0;

        while (progressed < seconds) {
            progressed += Time.deltaTime;
            yield return null;
        }

        action();
    }

    /// <summary>
    /// Performs an action after an amount of seconds.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="action"></param>
    public void After(float seconds, System.Action action)
    {
        StartCoroutine(Timer(seconds, action));
    }
}
